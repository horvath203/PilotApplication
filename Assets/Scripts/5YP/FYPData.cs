using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FYPData : MonoBehaviour
{
    [SerializeField]
    private GameObject summaryScreen;
    [SerializeField]
    private GameObject planningScreen;

    [SerializeField]
    private int[] values = new int[4];


    public Summary GetSummary()
    {
        return summaryScreen.GetComponent<Summary>();
    }

    public void Launch()
    {
        summaryScreen.GetComponent<Summary>().DisplayData(values);

        planningScreen.SetActive(false);
        summaryScreen.SetActive(true);

    }

    public void Entry(int index, string val)
    {
        values[index] = int.Parse(val);
    }

    public void SummarySkip()
    {
        summaryScreen.SetActive(false);
        planningScreen.SetActive(true);
    }
}
