using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegValues : MonoBehaviour
{
    //population variables
    [SerializeField]
    private Text totalPop;
    [SerializeField]
    private Text workersPop;
    [SerializeField]
    private Text foodProd;

    //other variables
    [SerializeField]
    private Text nextVarVal;


    public void SetPopulation(int total, int freeWorkers, int totalWorkers, int foodProduction)
    {
        totalPop.text = total.ToString();
        workersPop.text = freeWorkers.ToString() + "/" + totalWorkers.ToString();
        foodProd.text = foodProduction.ToString() + " | " + (total - totalWorkers).ToString();
    }

    public void SetNextVar(int value)
    {
        nextVarVal.text = value.ToString();
    }
}
