using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Showcasable))]

public class BuildingE : MonoBehaviour, Power
{
    [SerializeField]
    private int ironCost;
    [SerializeField]
    private int requiredWorkers;

    [SerializeField]
    private int energyProduction;

    //COSTS AND CONDITIONS

    public int IronCost()
    {
        return ironCost;
    }

    public int RequiredWorkers()
    {
        return requiredWorkers;
    }

    public bool TakesSlot()
    {
        return true;
    }

    //PRODUCTIONS

    public int EnergyProduction()
    {
        return energyProduction;
    }

    public string StringProductions()
    {
        string prod = "Energy: " + EnergyProduction().ToString();
        return prod;
    }

    // CORE FUNCTIONS

    public bool CanBuild()
    {
        CountryManager cm = CountryManager.Instance;
        RegionHandler reg = cm.GetSelectedRegion();

        if (reg.AvailablePopulation() >= requiredWorkers &&
           cm.GetIron() >= ironCost
           )
        {
            return true;
        }
        return false;
    }

    public void ApplyConstructionCosts(ref CountryManager.Resources res)
    {
        res.iron -= ironCost;
        res.maxEnergy += EnergyProduction();
    }

    public void ApplyProductions(ref CountryManager.Resources res)
    {

    }

    /*
    public void IncreaseProductions(ref RegionHandler.Region reg)
    {
        reg.busyWorkers += requiredWorkers;
        reg.energyProduction += energyProduction;
    }

    public void RemoveProductions(ref RegionHandler.Region reg)
    {
        reg.busyWorkers -= requiredWorkers;
        reg.energyProduction -= energyProduction;
    }
    */
}
