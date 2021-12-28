using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBuildings : MonoBehaviour
{
    // ----------------------------   JRD   ----------------------------
    //in each region, the remaining population (farmers) works at (multiplier * 100)% efficiency

    public int foodProduction;
    public float multiplier = 1;

    public void UpgradeJRD()
    {
        multiplier += 0.5f;
    }

    public int JRDProduction()
    {
        int farmers = gameObject.GetComponent<RegionHandler>().RemainingPopulation();

        foodProduction = (int) (farmers * multiplier);
        return foodProduction;
    }
}
