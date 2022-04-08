using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Showcasable))]

public class BuildingA : MonoBehaviour, Acommodation
{
    [SerializeField]
    private int energyCost;
    [SerializeField]
    private int ironCost;

    [SerializeField]
    private int providedPopulation;

    //COSTS AND CONDITIONS

    public int EnergyyCost()
    {
        return energyCost;
    }

    public int IronCost()
    {
        return ironCost;
    }

    public int RequiredWorkers()
    {
        return 0;
    }

    public bool TakesSlot()
    {
        return false;
    }

    //PRODUCTIONS

    public int ProvidedPopulation()
    {
        return providedPopulation;
    }

    public string StringProductions()
    {
        return "";
    }



    public bool CanBuild()
    {
        CountryManager cm = CountryManager.Instance;
        RegionHandler reg = cm.GetSelectedRegion();

        if(reg.RemainingPopulation() >= providedPopulation  &&
           cm.AvailableEnergy() >= energyCost                     &&
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

    }

    /*
    public void IncreaseProductions(ref RegionHandler.Region reg)
    {
        reg.workerPopulation += providedPopulation;
        //reg.moneyProduction -= maintenance;
    }

    public void RemoveProductions(ref RegionHandler.Region reg)
    {
        reg.workerPopulation -= providedPopulation;
        //reg.moneyProduction += maintenance;
    }
    */
}
