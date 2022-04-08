using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBuildings : MonoBehaviour
{
    // ----------------------------   JRD   ----------------------------
    //in each region, the remaining population (farmers) works at (multiplier * 100)% efficiency

    int foodProduction;
    [SerializeField]
    int jrdLevel = 1;
    float jrdmulti = 1;

    public int JRDLevel()
    {
        return jrdLevel;
    }

    public void UpgradeJRD()
    {
        jrdLevel++;
        jrdmulti += 0.5F;
    }

    public int JRDProduction()
    {
        int farmers = gameObject.GetComponent<RegionHandler>().RemainingPopulation();

        foodProduction = (int) (farmers * jrdmulti);
        return foodProduction;
    }

    // ----------------------------   Infrastructure   ----------------------------
    //in each region, the remaining population (farmers) works at (multiplier * 100)% efficiency

    [SerializeField]
    int infLevel = 1;
    float infmult = 1;

    public int INFLevel()
    {
        return infLevel;
    }

    public int INFCost()
    {
        return infLevel * infLevel;
    }

    public void UpgradeINF()
    {
        infLevel++;
        infmult += 0.5F;
    }

    public float INFMultiplier()
    {
        return infmult;
    }
}
