using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Showcasable))]

public class BuildingD : MonoBehaviour, Tazka
{
    //costs - required to build
    [SerializeField]
    private int moneyCost;
    [SerializeField]
    private int ironCost;
    [SerializeField]
    private int requiredWorkers;

    //productions - increases production
    //expenses - reduce production
    [SerializeField]
    private int traktorProduction;
    [SerializeField]
    private int tankProduction;
    [SerializeField]
    private int ironExpense;

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

    public int IronExpense()
    {
        return ironExpense;
    }

    public int TraktorProduction()
    {
        return traktorProduction;
    }

    public int TankProduction()
    {
        return tankProduction;
    }

    public string StringProductions()
    {
        string prod = "Traktors: " + traktorProduction.ToString();
        prod += "\n";
        prod += "Iron Costs: " + ironExpense.ToString();
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
        reg.traktorsProduction += traktorProduction;
        reg.ironProduction -= ironExpense;

        return reg;
    }

    public RegionHandler.Region RemoveProductions(RegionHandler.Region reg)
    {
        reg.busyWorkers -= requiredWorkers;
        reg.traktorsProduction -= traktorProduction;
        reg.ironProduction += ironExpense;

        return reg;
    }
}
