using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Showcasable))]

public class BuildingB : MonoBehaviour, Mines
{
    [SerializeField]
    private int energyCost;
    [SerializeField]
    private int ironCost;
    [SerializeField]
    private int requiredWorkers;

    [SerializeField]
    private int ironProduction;

    //COSTS AND CONDITIONS

    public int EnergyCost()
    {
        return energyCost;
    }

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

    public int IronProduction()
    {
        return ironProduction;
    }

    public string StringProductions()
    {
        string prod = "Iron: " + IronProduction().ToString();
        return prod;
    }



    public bool CanBuild()
    {
        CountryManager cm = CountryManager.Instance;
        RegionHandler reg = cm.GetSelectedRegion();

        if (reg.AvailablePopulation() >= requiredWorkers &&
           cm.AvailableEnergy() >= energyCost &&
           cm.GetIron() >= ironCost
           )
        {
            return true;
        }

        return false;
    }

    public void ApplyConstructionCosts(ref CountryManager.Resources res)
    {
        res.usedEnergy += energyCost;
        res.iron -= ironCost;
    }

    public void ApplyProductions(ref CountryManager.Resources res)
    {
        res.iron += IronProduction();
    }

    /*
    public void IncreaseProductions(ref RegionHandler.Region reg)
    {
        reg.busyWorkers += requiredWorkers;
        reg.ironProduction += ironProduction;
    }

    public void RemoveProductions(ref RegionHandler.Region reg)
    {
        reg.busyWorkers -= requiredWorkers;
        reg.ironProduction -= ironProduction;
    }
    */
}
