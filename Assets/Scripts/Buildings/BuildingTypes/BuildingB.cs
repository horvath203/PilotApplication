using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Showcasable))]

public class BuildingB : MonoBehaviour, Mines
{
    [SerializeField]
    private int moneyCost = 0;
    [SerializeField]
    private int ironCost = 0;
    [SerializeField]
    private int requiredWorkers = 0;

    [SerializeField]
    private int ironProduction = 0;

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

    public int IronProduction()
    {
        return ironProduction;
    }

    public string StringProductions()
    {
        string prod = "Iron: " + ironProduction.ToString();
        return prod;
    }



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
        reg.ironProduction += ironProduction;

        return reg;
    }

    public RegionHandler.Region RemoveProductions(RegionHandler.Region reg)
    {
        reg.busyWorkers -= requiredWorkers;
        reg.ironProduction -= ironProduction;

        return reg;
    }
}
