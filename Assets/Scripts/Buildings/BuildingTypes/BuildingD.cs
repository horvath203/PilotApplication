using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Showcasable))]

public class BuildingD : MonoBehaviour, Tazka
{
    //costs - required to build
    [SerializeField]
    private int energyCost;
    [SerializeField]
    private int ironCost;
    [SerializeField]
    private int requiredWorkers;

    //productions - increases production
    //expenses - reduce production
    [SerializeField]
    private int ironExpense;
    [SerializeField]
    private int traktorProduction;

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

    public int IronExpense()
    {
        return ironExpense;
    }

    public int TraktorProduction()
    {
        return traktorProduction;
    }

    public string StringProductions()
    {
        string prod = "Traktors: " + TraktorProduction().ToString();
        prod += "\n";
        prod += "Iron Costs: " + IronExpense().ToString();
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

    public bool ExpensesAvailable(CountryManager.Resources res)
    {
        bool availability = true;
        if (res.iron < IronExpense()) availability = false;

        return availability;
    }

    public void ApplyProductions(ref CountryManager.Resources res)
    {
        if (ExpensesAvailable(res))
        {
            res.iron -= IronExpense();
            res.traktors += TraktorProduction();
        }
    }

    /*
    public void IncreaseProductions(ref RegionHandler.Region reg)
    {
        reg.busyWorkers += requiredWorkers;
        reg.traktorsProduction += traktorProduction;
        reg.ironProduction -= ironExpense;
    }

    public void RemoveProductions(ref RegionHandler.Region reg)
    {
        reg.busyWorkers -= requiredWorkers;
        reg.traktorsProduction -= traktorProduction;
        reg.ironProduction += ironExpense;
    }
    */
}
