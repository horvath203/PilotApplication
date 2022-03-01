using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Showcasable))]

public class BuildingC : MonoBehaviour, Lahka
{
    [SerializeField]
    private int moneyCost;
    [SerializeField]
    private int ironCost;
    [SerializeField]
    private int requiredWorkers;

    [SerializeField]
    private int moneyProduction;
    [SerializeField]
    private int electronicProduction;
    [SerializeField]
    private int chemicalProduction;
    [SerializeField]
    private int textilProduction;

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

    public int MoneyProduction()
    {
        return moneyProduction;
    }

    public int ElectronicProduction()
    {
        return electronicProduction;
    }

    public int ChemicalProduction()
    {
        return chemicalProduction;
    }

    public int TextilProduction()
    {
        return textilProduction;
    }

    public string StringProductions()
    {
        string prod = "Money: " + moneyProduction.ToString();
        prod += "\n";
        prod += "Electronics: " + electronicProduction.ToString();
        prod += "\n";
        prod += "Chemicals: " + chemicalProduction.ToString();
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
        reg.moneyProduction += moneyProduction;

        return reg;
    }

    public RegionHandler.Region RemoveProductions(RegionHandler.Region reg)
    {
        reg.busyWorkers -= requiredWorkers;
        reg.moneyProduction -= moneyProduction;

        return reg;
    }
}
