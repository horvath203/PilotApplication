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
    private int habitates;

    [SerializeField]
    private List<PassiveText> passiveTexts = new List<PassiveText>();

    [System.Serializable]
    public struct Region
    {
        public string name;
        public int totalPopulation;
        public int workerPopulation;
        public int busyWorkers;
        public int nextVar;
        public float satisfaction;
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
        region.satisfaction = 1.5F;

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

    public int EnergyProduction()
    {
        return 0;
    }

    public int IronProduction()
    {
        return 0;
    }

    public int TraktorProduction()
    {
        return 0;
    }

    public int TextilProduction()
    {
        return 0;
    }

    public int FoodProduction()
    {
        return gameObject.GetComponent<PassiveBuildings>().JRDProduction();
    }

    private float ProductionQuatificator()
    {
        return (region.satisfaction * gameObject.GetComponent<PassiveBuildings>().INFMultiplier());
    }

    public void Produce(ref CountryManager.Resources res)
    {
        foreach (StructureSlot slot in buildingSlots)
        {
            slot.ApplyProductions(ref res, ProductionQuatificator());
        }
    }

    /*private int QuantifiedProduction(int production)
    {
        return (int)((production * region.satisfaction) * gameObject.GetComponent<PassiveBuildings>().INFMultiplier());
    }*/

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

    public bool HasAvailableSlot()
    {
        if (BuildingCapacity() > OccupiedBuildingSlots())
        {
            return true;
        }
        else return false;
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

        region.workerPopulation += habitate.GetComponent<Acommodation>().ProvidedPopulation();
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

    public void SetPassiveShowcase()
    {
        string txt = habitates.ToString();
        passiveTexts[0].ChangeText(txt);

        txt = GetComponent<PassiveBuildings>().INFLevel().ToString();
        passiveTexts[1].ChangeText(txt);

        txt = GetComponent<PassiveBuildings>().JRDLevel().ToString();
        passiveTexts[2].ChangeText(txt);
    }

    public void CancelPlannedBuilding(StructureSlot slot)
    {
        GameObject tbb = CountryManager.Instance.GetBuildingSlot();

        slot.CancelConstruction(
            Instantiate(tbb, slot.GetTransform().position, slot.GetTransform().rotation, transform));
    }

    public void RestoreBuilding(StructureSlot slot)
    {
        slot.Restore(ref region);
    }

    // ------------------- END TURN -------------------------

    public void CheckRuin()
    {
        foreach (StructureSlot slot in buildingSlots)
        {
            if ( (!slot.IsRuined()) && slot.Age() > CountryManager.Instance.RuinTimer())
            {
                //Debug.Log("Where is SHE?!");
                slot.Ruin(ref region);
            }
        }
    }

    public void EndBuildingTurn()
    {
        foreach (StructureSlot slot in buildingSlots)
        {
            //buildings that are not yet built
            if (!slot.IsPermanent() && (AvailablePopulation() >= slot.RequiredWorkers()) )
            {
                slot.ConfirmBuilding();
                region.busyWorkers += slot.GetBuilding().RequiredWorkers();
            }
        }
    }

    void OnDrawGizmos()
    {
        region.name = name;
        this.tag = "Region";
    }
}
