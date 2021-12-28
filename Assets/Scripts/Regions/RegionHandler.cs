using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(RegionClick))]
[RequireComponent(typeof(PassiveBuildings))]

public class RegionHandler : MonoBehaviour
{
    [SerializeField]
    private Region region;

    [SerializeField]
    private List<StructureSlot> buildingSlots = new List<StructureSlot>();

    [SerializeField]
    private int habitates = 0;

    [System.Serializable]
    public struct Region
    {
        //attributes
        public string name;
        public int totalPopulation;
        public int workerPopulation;
        public int busyWorkers;
        public int nextVar;
        public float satisfaction;
        //productions
        public int moneyProduction;
        public int ironProduction;
        public int traktorsProduction;
    }
    

    // ---------- INITIATION ----------

    public void InitiateRegion()
    {
        // building slots
        foreach (Transform child in transform)
        {
            if (child.tag == "BuildingSlot")
            {
                StructureSlot slot = new StructureSlot(child);
                buildingSlots.Add(slot);
            }
        }

        // general attributes
        region.satisfaction = 1;

    }


    // ------ ATTRIBUTES --------

    public string Name()
    {
        return region.name;
    }

    //all people in region
    public int TotalPopulation()
    {
        return region.totalPopulation;
    }

    //all people who have become workers
    public int WorkerPopulation()
    {
        return region.workerPopulation;
    }

    //workers that are not working
    public int AvailablePopulation()
    {
        return region.workerPopulation - region.busyWorkers;
    }

    //all the people who are not workers - aka farmers
    public int RemainingPopulation()
    {
        return region.totalPopulation - region.workerPopulation;
    }

    public int NextVar()
    {
        return region.nextVar;
    }


    // --------- PRODUCTIONS ----------

    public int MoneyProduction()
    {
        return region.moneyProduction;
    }

    public int IronProduction()
    {
        return region.ironProduction;
    }

    public int TraktorProduction()
    {
        return region.traktorsProduction;
    }

    public int FoodProduction()
    {
        return gameObject.GetComponent<PassiveBuildings>().JRDProduction();
    }

    public CountryManager.Resources UpdateResources(CountryManager.Resources res)
    {
        res.money += QuantifiedProduction(region.moneyProduction);
        res.iron += QuantifiedProduction(region.ironProduction);
        res.traktors += QuantifiedProduction(region.traktorsProduction);

        return res;
    }

    private int QuantifiedProduction(int production)
    {
        return (int)(production * region.satisfaction);
    }

    public void Starving(float mult)
    {
        region.satisfaction = mult;
    }


    // ---------- BUILDING SLOTS ------------

    public StructureSlot GetSlot(int i)
    {
        return buildingSlots[i];
    }

    public int BuildingCapacity()
    {
        return buildingSlots.Count;
    }

    public int OccupiedBuildingSlots()
    {
        int res = 0;
        foreach (StructureSlot slot in buildingSlots)
        {
            if (slot.IsOccupied())
            {
                res++;
            }
        }
        return res;
    }

    public void BuildHabitate(GameObject habitate)
    {
        habitates++;

        region = habitate.GetComponent<BuildingType>().IncreaseProductions(region);
    }

    public void BuildStructure(GameObject tbb)
    {
        foreach (StructureSlot slot in buildingSlots)
        {
            if (!slot.IsOccupied())
            {
                slot.Build(
                    Instantiate(tbb, slot.GetTransform().position, slot.GetTransform().rotation, transform));
                break;
            }
        }
    }

    public void CancelPlannedBuilding(StructureSlot slot)
    {
        GameObject tbb = CountryManager.Instance.GetBuildingSlot();

        slot.CancelConstruction(
            Instantiate(tbb, slot.GetTransform().position, slot.GetTransform().rotation, transform));
    }

    public void RestoreBuilding(StructureSlot slot)
    {
        region = slot.Restore(region);
    }

    // ------------------- END TURN -------------------------

    public void CheckRuin()
    {
        foreach (StructureSlot slot in buildingSlots)
        {
            if ( (!slot.IsRuined()) && slot.Age() > CountryManager.Instance.RuinTimer())
            {
                Debug.Log("Where is SHE?!");
                region = slot.Ruin(region);
            }
        }
    }

    public void EndBuildingTurn()
    {
        foreach (StructureSlot slot in buildingSlots)
        {
            //buildings that are not yet built
            if (!slot.IsPermanent())
            {
                region = slot.ConfirmBuilding(region);
            }
        }
    }

    void OnDrawGizmos()
    {
        region.name = name;
        this.tag = "Region";
    }
}
