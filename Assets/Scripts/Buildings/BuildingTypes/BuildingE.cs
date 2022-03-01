using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Showcasable))]

public class BuildingE : MonoBehaviour, Power
{
    [SerializeField]
    private int moneyCost;
    [SerializeField]
    private int ironCost;
    [SerializeField]
    private int requiredWorkers;

    [SerializeField]
    private int maintenance;
    [SerializeField]
    private int powerProduction;

    //COSTS AND CONDITIONS

    public int MoneyCost()
    {
        return moneyCost;
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

    public int Maintenance()
    {
        return maintenance;
    }

    public int PowerProduction()
    {
        return powerProduction;
    }

    public string StringProductions()
    {
        string prod = "Maintenance: " + maintenance.ToString();
        prod += "\n";
        prod += "Power: " + powerProduction.ToString();
        return prod;
    }

    // CORE FUNCTIONS

    public bool CanBuild()
    {
        CountryManager cm = CountryManager.Instance;
        RegionHandler reg = cm.GetSelectedRegion();

        if (reg.AvailablePopulation() >= requiredWorkers &&
           cm.GetMoney() >= moneyCost &&
           cm.GetIron() >= ironCost
           )
        {
            return true;
        }

        return false;
    }

    public CountryManager.Resources ApplyConstructionCosts(CountryManager.Resources res)
    {
        res.money -= moneyCost;
        res.iron -= ironCost;

        return res;
    }

    public RegionHandler.Region IncreaseProductions(RegionHandler.Region reg)
    {
        reg.busyWorkers += requiredWorkers;
        reg.moneyProduction -= maintenance;

        return reg;
    }

    public RegionHandler.Region RemoveProductions(RegionHandler.Region reg)
    {
        reg.busyWorkers -= requiredWorkers;
        reg.moneyProduction += maintenance;

        return reg;
    }
}
