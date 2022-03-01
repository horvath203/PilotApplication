using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegDetails : MonoBehaviour
{
    [SerializeField]
    private Text regionName;

    //Values panel
    [SerializeField]
    private RegValues regionValues;
    [SerializeField]
    private BuildValues buildingValues;

    //other data variables
    [SerializeField]
    private Text bSlots;
    [SerializeField]
    private BuildingMenu bmenu;
    [SerializeField]
    private ShowcaseMenu showcase;


    public void Start()
    {
        HideBuildingMenu();
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public ShowcaseMenu GetShowcase()
    {
        return showcase;
    }

    public bool BuildingDetailsActive()
    {
        return buildingValues.gameObject.activeSelf;
    }

    public void Activate()
    {
        if (!IsActive())
        {
            gameObject.SetActive(true);
        }
    }

    public void Deactivate()
    {
        if (IsActive())
        {
            gameObject.SetActive(false);
        }
    }

    // setting region details display

    public void SetRegionDetails(RegionHandler reg)
    {
        SetName(reg.Name());
        regionValues.SetPopulation(reg.TotalPopulation(), reg.AvailablePopulation(), reg.WorkerPopulation(), reg.FoodProduction());
        regionValues.SetNextVar(reg.NextVar());
        SetBuildingSlots(BuildingsDetails(reg));
    }

    private void SetName(string name)
    {
        regionName.text = name;
    }

    private void SetBuildingSlots(string buildings)
    {
        bSlots.text = buildings;
    }

    private string BuildingsDetails(RegionHandler reg)
    {
        string buildings = reg.OccupiedBuildingSlots().ToString();
        buildings = buildings + "/";
        buildings = string.Concat(buildings, reg.BuildingCapacity().ToString());
        return buildings;
    }


    // Building Details

    public void CloseBuildingDetails()
    {
        DisableBuildingDetails();
    }

    public void CallBuildingDetails(SCDisplaySlot slot)
    {
        buildingValues.SetBuildingDetails(slot.GetStructureSlot());
        ActivateBuildingDetails();
    }


    // Object Active status

    private void ActivateBuildingDetails()
    {
        regionValues.gameObject.SetActive(false);
        buildingValues.gameObject.SetActive(true);
    }

    private void DisableBuildingDetails()
    {
        buildingValues.gameObject.SetActive(false);
        regionValues.gameObject.SetActive(true);
    }


    //Building Menu Section

    public bool BMenuActive()
    {
        return bmenu.gameObject.activeSelf;
    }

    public void ShowBuildingMenu()
    {
        if (BMenuActive())
        {
            HideBuildingMenu();
        }
        else
        {
            SetButtons();
            bmenu.gameObject.SetActive(true);
        }
    }

    private void SetButtons()
    {
        int money = CountryManager.Instance.GetMoney();
        int iron = CountryManager.Instance.GetIron();
        RegionHandler reg = CountryManager.Instance.GetSelectedRegion();

        foreach(GameObject button in bmenu.buildButtons)
        {
            BuildingType building = button.GetComponent< ShowcaseLink >().GetLink().GetComponent< BuildingType >();

            if ( (!building.TakesSlot() || CountryManager.Instance.GetSelectedRegion().HasAvailableSlot()) && 
                building.CanBuild() )
            {
                button.SetActive(true);
            }
            else
            {
                button.SetActive(false);
            }
        }
    }

    public void HideBuildingMenu()
    {
        bmenu.gameObject.SetActive(false);
    }
}
