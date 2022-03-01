using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summary : MonoBehaviour
{
    [SerializeField]
    private int[] generated = new int[4];

    [SerializeField]
    private List<Text> display;

    public void DisplayData(int[] values)
    {
        for(int i = 0; i < 3; i++)
        {
            string text = generated[i].ToString();
            text += " / ";
            text += values[i].ToString();

            display[i].text = text;
        }
    }

    public void AddProduced(RegionHandler reg)
    {
        CountryManager.Resources res;
        res.money = 0;
        res.iron = 0;
        res.traktors = 0;   
        res = reg.UpdateResources(res);

        generated[0] = res.money;
        generated[1] = res.iron;
        generated[2] = res.traktors;
    }
}
