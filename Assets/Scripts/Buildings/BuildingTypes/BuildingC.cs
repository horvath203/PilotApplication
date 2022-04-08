using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Showcasable))]

public class BuildingC : MonoBehaviour, Lahka
{
    [SerializeField]
    private int energyCost;
    [SerializeField]
    private int ironCost;
    [SerializeField]
    private int requiredWorkers;

    [SerializeField]
    private int textilProduction;

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

    public int TextilProduction()
    {
        return textilProduction;
    }

    public string StringProductions()
    {
        string prod = "Textil: " + TextilProduction().ToString();
        return prod;
    }

    // CORE FUNCTIONS

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
        res.textil += TextilProduction();
    }

    /*
    public void IncreaseProductions(ref RegionHandler.Region reg)
    {
        reg.busyWorkers += requiredWorkers;
        reg.energyProduction += moneyProduction;
    }

    public void RemoveProductions(ref RegionHandler.Region reg)
    {
        reg.busyWorkers -= requiredWorkers;
        reg.energyProduction -= moneyProduction;
    }
    */
}
