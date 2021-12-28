using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryManager : MonoBehaviour
{
    private static CountryManager _instance;

    //REGIONS

    public RegDetails detailsui;

    private RegionHandler selectedRegion;

    public List<RegionHandler> regionList = new List<RegionHandler>();

    //GLOBAL RESOURCES

    [SerializeField]
    private Resources resources;

    private int turn = 1;

    public List<Text> globalResources;


    [System.Serializable]
    public struct Resources
    {
        public int money;
        public int iron;
        public int traktors;
        public int food;
    }

    //BUILDINGS

    private StructureSlot displayedSlot;

    public GameObject emptyBuildingSlot;

    public GameObject accomUnit;

    private int buildingsPerTurn = 5;

    private int builtThisTurn = 0;

    [SerializeField]
    private int ruinTimer;


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
    }

    //refference to a prefab
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

    public int GetMoney()
    {
        return resources.money;
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
        return resources.food;
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
        resources.money = 650;
        resources.iron = 7;
        UpdateGlobalResources();
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
        if (detailsui.BMenuActive())
        {
            detailsui.HideBuildingMenu();
        }

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


    //show and refresh need to be just one method, ready to be used whether or not a region is selected, hiding bmenu, 
    //and hiding building details,showcasing region details instead
    private void RefreshDetails()
    {
        if (detailsui.BMenuActive()) {
            detailsui.HideBuildingMenu();
        }

        if (detailsui.BuildingDetailsActive())
        {
            detailsui.CloseBuildingDetails();
        }

        if(selectedRegion != null)
        {
            detailsui.SetRegionDetails(selectedRegion);
            SetShowcase();
            UpdateGlobalResources();
            detailsui.Activate();
        }
        else
        {
            detailsui.Deactivate();
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
        globalResources[0].text = resources.money.ToString();
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
        resources.food = food;

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
        if (selectedRegion.OccupiedBuildingSlots() >= selectedRegion.BuildingCapacity())
        {
            return "no build slots available in this region";
        }
        else if (builtThisTurn >= buildingsPerTurn)
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
        resources = building.ApplyConstructionCosts(resources);

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



    // ------------ END TURN SECTION -----------


    public void EndTurn()
    {
        ManageBuildings();

        Verify5YP();

        UpdateGlobalResources();

        CancelRegionSelection();
    }

    private void ManageBuildings()
    {
        foreach (RegionHandler reg in regionList)
        {
            reg.CheckRuin();
            resources = reg.UpdateResources(resources);
            reg.EndBuildingTurn();
        }
    }

    //calculate how many resources were gained
    private void Verify5YP()
    {
        if (turn % 5 == 0)
        {
            builtThisTurn = 0;
        }
        turn++;
    }
}
