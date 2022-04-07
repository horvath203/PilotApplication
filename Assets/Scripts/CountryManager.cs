using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountryManager : MonoBehaviour
{
    private static CountryManager _instance;

    public static CountryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CountryManager>();
            }

            return _instance;
        }
    }

    void awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        RefreshDetails();
        ListRegions();
        SetStartingResources();
        foreach(RegionHandler reg in regionList)
        {
            reg.SetPassiveShowcase();
        }
    }

    //UI interface

    //private GameObject WholeUI;

    [SerializeField]
    private FYPData FYP;

    [SerializeField]
    private GameObject mainCam;

    [SerializeField]
    private GameObject fypCam;

    //REGIONS

    public RegDetails detailsui;

    private RegionHandler selectedRegion;

    public List<RegionHandler> regionList = new List<RegionHandler>();

    //GLOBAL RESOURCES

    [SerializeField]
    private Resources resources;

    private int food;

    private int turn = 1;

    public List<Text> globalResources;


    [System.Serializable]
    public struct Resources
    {
        public int maxEnergy;
        public int usedEnergy;
        public int iron;
        public int traktors;
        public int textil;
    }



    //------- BEGGINING SETUP ----------


    void ListRegions()
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag("Region") as GameObject[];
        foreach (GameObject obj in array)
        {
            RegionHandler reg = obj.GetComponent<RegionHandler>();
            reg.InitiateRegion();

            regionList.Add(reg);
        }
    }

    private void SetStartingResources()
    {
        resources.maxEnergy = 650;
        resources.iron = 7;
        UpdateGlobalResources();
    }


    //BUILDINGS

    private StructureSlot displayedSlot;

    public GameObject emptyBuildingSlot;

    public GameObject accomUnit;

    private int buildingsPerTurn = 5;

    private int builtThisTurn = 0;

    [SerializeField]
    private int ruinTimer;




    //refference to a BuildingSlot prefab
    public GameObject GetBuildingSlot()
    {
        return emptyBuildingSlot;
    }

    //SHOULD THIS BE A THING?
    public RegionHandler GetSelectedRegion()
    {
        return selectedRegion;
    }

    public int CurrentTurn()
    {
        return turn;
    }

    public int RuinTimer()
    {
        return ruinTimer;
    }

    public int TotalEnergy()
    {
        return resources.maxEnergy;
    }

    public int AvailableEnergy()
    {
        return (resources.maxEnergy - resources.usedEnergy);
    }

    public int GetIron()
    {
        return resources.iron;
    }

    public int GetTraktors()
    {
        return resources.traktors;
    }

    public int GetFoodProd()
    {
        return food;
    }

    public int GlobalPopulation()
    {
        int people = 0;
        foreach(RegionHandler reg in regionList)
        {
            people += reg.TotalPopulation();
        }

        return people;
    }



    // --------------- REGION SELECTION ---------------


    //declared triggering region as selected, or unselect it if it had been selected already
    public void SelectRegion(RegionHandler regHandler)
    {
        RegionHandler old = selectedRegion;
        CancelRegionSelection();

        if (regHandler != old)
        {
            selectedRegion = regHandler;
            selectedRegion.GetComponent<RegionClick>().SetSelected();
            RefreshDetails();
        }
    }

    //unselects region, hides reg. det. UI
    public void CancelRegionSelection()
    {
        HideBMenu();

        if (selectedRegion != null)
        {
            selectedRegion.GetComponent<RegionClick>().SetUnselected();
            selectedRegion = null;
        }

        RefreshDetails();
    }

    private void Starving(float mult)
    {
        foreach (RegionHandler reg in regionList)
        {
            reg.Starving(mult);
        }
    }



    // --------------- UI DISPLAY ---------------


    public void SwitchToMainCam()
    {
        fypCam.SetActive(false);
        mainCam.SetActive(true);
    }

    private void SwitchToFYPCam()
    {
        mainCam.SetActive(false);
        fypCam.SetActive(true);
        FYP.Launch();
    }


    //show and refresh need to be just one method, ready to be used whether or not a region is selected, hiding bmenu, 
    //and hiding building details,showcasing region details instead
    private void RefreshDetails()
    {
        HideBMenu();
        HideBDetail();

        if (selectedRegion != null)
        {
            detailsui.SetRegionDetails(selectedRegion);
            selectedRegion.SetPassiveShowcase();
            SetShowcase();
            UpdateGlobalResources();
            detailsui.Activate();
        }
        else
        {
            detailsui.Deactivate();
        }
    }

    private void HideBDetail()
    {
        if (detailsui.BuildingDetailsActive())
        {
            detailsui.CloseBuildingDetails();
        }
    }

    private void HideBMenu()
    {
        if (detailsui.BMenuActive())
        {
            detailsui.HideBuildingMenu();
        }
    }

    private void SetShowcase()
    {
        ShowcaseMenu showcase = detailsui.GetShowcase();
        showcase.ClearDisplay();

        for (int i = 0; i < selectedRegion.BuildingCapacity(); i++)
        {
            StructureSlot slot = selectedRegion.GetSlot(i);
            showcase.DisplaySlot(slot, i);
        }
    }

    //updates display of global resources to stored values
    private void UpdateGlobalResources()
    {
        string availableEnergy = (resources.maxEnergy - resources.usedEnergy).ToString();
        globalResources[0].text = availableEnergy + "/" + resources.maxEnergy.ToString();
        globalResources[1].text = resources.iron.ToString();
        globalResources[2].text = FoodProduction().ToString();
        globalResources[3].text = turn.ToString();
    }

    private int FoodProduction()
    {
        int food = 0;
        foreach (RegionHandler reg in regionList)
        {
            food += reg.FoodProduction();
        }

        //update satisfaction
        int totalpop = GlobalPopulation();
        if (food < totalpop)
        {
            if ((2 * food) < totalpop) Starving(0.5f);
            else Starving(0.8f);
        }
        else Starving(1);

        return food;
    }



    // ---------------- BUILDING SECTION -------------


    //prompts region to open building menu (if available)
    public void CallBuildingMenu()
    {
        string error = BuildError();
        if (error == "")
        {
            detailsui.ShowBuildingMenu();
        }
        else
        {
            //CALL ERROR "CAN NOT BUILD IN THIS REGION"
            Debug.Log("Can not open the menu, " + error);
        }
    }

    private string BuildError()
    {
        if (builtThisTurn >= buildingsPerTurn)
        {
            return "build limit reached this turn.";
        }
        return "";
    }

    public void CallBuildingDetails(SCDisplaySlot slot)
    {
        displayedSlot = slot.GetStructureSlot();
        detailsui.CallBuildingDetails(slot);
    }

    public void CloseBuildingDetails()
    {
        detailsui.CloseBuildingDetails();
        RefreshDetails();
        displayedSlot = null;
    }

    //for building structures that do not take up slots
    //currently fixed to build accomUnit(BuildingA), should change to allow multiple accom types
    public void Build()
    {
        selectedRegion.BuildHabitate(accomUnit);
        CommitStructure( accomUnit.GetComponent<BuildingType>() );
    }

    //For buildings structures that take up Slots
    public void Build(ShowcaseLink buildingObject)
    {
        GameObject building = buildingObject.GetLink();

        selectedRegion.BuildStructure(building);
        CommitStructure( building.GetComponent<BuildingType>() );
    }

    public void CommitStructure(BuildingType building)
    {
        building.ApplyConstructionCosts(ref resources);

        builtThisTurn++;
        RefreshDetails();
    }

    public void CancelPlannedBuilding()
    {
        selectedRegion.CancelPlannedBuilding(displayedSlot);
        RefreshDetails();
    }

    public void RestoreBuilding()
    {
        selectedRegion.RestoreBuilding(displayedSlot);
        //subtract restoration costs
        RefreshDetails();
    }


    public void UpgradeJRD()
    {
        if (resources.traktors > 0)
        {
            resources.traktors--;
            selectedRegion.gameObject.GetComponent<PassiveBuildings>().UpgradeJRD();
            RefreshDetails();
        }
        else
        {
            Debug.Log("Not enough traktors.");
        }
    }

    public void UpgradeINF()
    {
        int upgradeCost = selectedRegion.gameObject.GetComponent<PassiveBuildings>().INFCost();
        if (resources.iron >= upgradeCost)
        {
            resources.iron -= upgradeCost;
            selectedRegion.gameObject.GetComponent<PassiveBuildings>().UpgradeINF();
            RefreshDetails();
        }
        else
        {
            Debug.Log("Can not upgrade INF");
        }
    }



    // ------------ END TURN SECTION -----------


    public void EndTurn()
    {
        CancelRegionSelection();
        ManageBuildings();

        Verify5YP();

        UpdateGlobalResources();
    }

    private void ManageBuildings()
    {
        foreach (RegionHandler reg in regionList)
        {
            reg.CheckRuin();
            reg.Produce(ref resources);
            FYP.GetSummary().AddProduced(reg);
            reg.EndBuildingTurn();
        }
    }

    //calculate how many resources were gained
    private void Verify5YP()
    {
        if (turn % 5 == 0)
        {
            builtThisTurn = 0;
            SwitchToFYPCam();
        }
        turn++;
    }
}
