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

    public int MoneyProduction()
    {
        //if(building is type Lahka) return building.MoneyProduction();
        //if(building is type Accomodation) return building.Maintenance();
        return 0;
    }

    public int IronProduction()
    {
        //if(building is type Mines) return building.IronProduction();
        //if(building is type Tazka) return building.IronExpense();
        return 0;
    }

    public int TraktorProduction()
    {
        //if(building is type Tazka) return building.TraktorProduction();
        return 0;
    }



    public RegionHandler.Region ConfirmBuilding(RegionHandler.Region reg)
    {
        building.GetComponent<DoubleLink>().SwapLink();
        permanent = true;
        builtDate = CountryManager.Instance.CurrentTurn();
        return GetBuilding().IncreaseProductions(reg);
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

    public int Age()
    {
        if (!IsOccupied())
        {
            return 0;
        }
        return CountryManager.Instance.CurrentTurn() - builtDate;
    }

    public RegionHandler.Region Ruin(RegionHandler.Region reg)
    {
        ruin = true;
        return GetBuilding().RemoveProductions(reg);
    }

    public RegionHandler.Region Restore(RegionHandler.Region reg)
    {
        ruin = false;
        builtDate = CountryManager.Instance.CurrentTurn();
        return GetBuilding().IncreaseProductions(reg);
    }
}