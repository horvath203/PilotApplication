using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBuildings : MonoBehaviour
{
    // ----------------------------   JRD   ----------------------------
    //in each region, the remaining population (farmers) works at (multiplier * 100)% efficiency

    int foodProduction;
    [SerializeField]
    float JRDLevel = 1;

    public void UpgradeJRD()
    {
        JRDLevel += 0.5f;
    }

    public int JRDProduction()
    {
        int farmers = gameObject.GetComponent<RegionHandler>().RemainingPopulation();

        foodProduction = (int) (farmers * JRDLevel);
        return foodProduction;
    }

    // ----------------------------   Infrastructure   ----------------------------
    //in each region, the remaining population (farmers) works at (multiplier * 100)% efficiency

    [SerializeField]
    int INFLevel = 1;
    float multiplier = 1;

    public int INFCost()
    {
        return INFLevel * INFLevel;
    }

    public void UpgradeINF()
    {
        INFLevel++;
        multiplier += 0.5F;
    }

    public float INFMultiplier()
    {
        return multiplier;
    }
}
