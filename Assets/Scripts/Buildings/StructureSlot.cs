using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StructureSlot
{
    private bool permanent;

    private GameObject building;

    private bool occupied;

    [SerializeField]
    private int builtDate;

    [SerializeField]
    private bool ruin;

    public StructureSlot(Transform trans)
    {
        building = trans.gameObject;
        permanent = true;
        occupied = false;
        ruin = false;
    }

    public BuildingType GetBuilding()
    {
        return building.GetComponent<BuildingType>();
    }

    public GameObject GetShowcaseIcon()
    {
        return building.GetComponent<Showcasable>().GetLink();
    }

    public bool IsPermanent()
    {
        return permanent;
    }

    public bool IsOccupied()
    {
        return occupied;
    }

    public bool IsRuined()
    {
        return ruin;
    }

    public Transform GetTransform()
    {
        return building.transform;
    }

    public int RequiredWorkers()
    {
        if (IsOccupied())
        {
            return building.GetComponent<BuildingType>().RequiredWorkers();
        }
        else return 0;
    }

    
    public int EnergyProduction()
    {
        Power power = GetBuilding() as Power;
        if(power != null) return power.EnergyProduction();
        return 0;
    }
    
    public int IronProduction()
    {
        Mines mine = GetBuilding() as Mines;
        if (mine != null) return mine.IronProduction();
        Tazka tazka = GetBuilding() as Tazka;
        if (tazka != null) return tazka.IronExpense();
        return 0;
    }

    public int TraktorProduction()
    {
        Tazka tazka = GetBuilding() as Tazka;
        if (tazka != null) return tazka.TraktorProduction();
        return 0;
    }

    public int TextilProduction()
    {
        Lahka lahka = GetBuilding() as Lahka;
        if (lahka != null) return lahka.TextilProduction();
        return 0;
    }
    



    public void Build(GameObject newBuilding)
    {
        if(building != null)
        {
            Object.Destroy(building);
        }

        occupied = true;
        permanent = false;
        building = newBuilding;
    }

    public void CancelConstruction(GameObject emptySlot)
    {
        Object.Destroy(building);

        occupied = false;
        permanent = true;
        building = emptySlot;
    }

    public void ConfirmBuilding()
    {
        building.GetComponent<DoubleLink>().SwapLink();
        permanent = true;
        builtDate = CountryManager.Instance.CurrentTurn();
    }

    public void ApplyProductions(ref CountryManager.Resources res, float quant)
    {
        if (!ruin)
        {
            res.iron += QuantifiedProduction(IronProduction(), quant);
            res.traktors += QuantifiedProduction(TraktorProduction(), quant);
            res.textil += QuantifiedProduction(TextilProduction(), quant);
        }
    }

    private int QuantifiedProduction(int prod, float quant)
    {
        return (int)(prod * quant);
    }

    public int Age()
    {
        if (!IsOccupied()) return 0;
        return CountryManager.Instance.CurrentTurn() - builtDate;
    }

    public void Ruin(ref RegionHandler.Region reg)
    {
        ruin = true;
    }

    public void Restore(ref RegionHandler.Region reg)
    {
        ruin = false;
        builtDate = CountryManager.Instance.CurrentTurn();
    }
}