using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Showcasable))]

public class BuildingA : MonoBehaviour, Acommodation
{
    [SerializeField]
    private int moneyCost = 0;
    [SerializeField]
    private int ironCost = 0;

    [SerializeField]
    private int maintenance = 0;
    [SerializeField]
    private int providedPopulation = 0;

    //COSTS AND CONDITIONS

    public int MoneyCost()
    {
        return moneyCost;
    }

    public int IronCost()
    {
        return ironCost;
    }

    //PRODUCTIONS

    public int Maintenance()
    {
        return maintenance;
    }

    public int ProvidedPopulation()
    {
        return providedPopulation;
    }

    public string StringProductions()
    {
        string prod = "Maintenance: " + maintenance.ToString();
        return prod;
    }



    public bool CanBuild()
    {
        CountryManager cm = CountryManager.Instance;
        RegionHandler reg = cm.GetSelectedRegion();

        if(reg.RemainingPopulation() >= providedPopulation  &&
           cm.GetMoney() >= moneyCost                            &&
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
        reg.workerPopulation += providedPopulation;
        reg.moneyProduction -= maintenance;

        return reg;
    }

    public RegionHandler.Region RemoveProductions(RegionHandler.Region reg)
    {
        reg.workerPopulation -= providedPopulation;
        reg.moneyProduction += maintenance;

        return reg;
    }
}
