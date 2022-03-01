using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PolygonCollider2D))]

public class RegionText : MonoBehaviour
{
    public Text textRegion;

    private GameObject mapRegion;

    private string regionName;


    void Start()
    {
        FindText();
        regionName = mapRegion.GetComponent<RegionHandler>().Name();
        textRegion.text = regionName;
        this.name = regionName + "Text";
    }

    public void FindText()
    {
        textRegion = transform.Find("Text").GetComponent<Text>();
    }

    public void SetMapRegion(GameObject reg)
    {
        mapRegion = reg;
    }

    public void SetColorSelected()
    {
        textRegion.color = mapRegion.GetComponent < RegionClick >().GetSelectedColor();
    }

    public void SetColorUnselected()
    {
        textRegion.color = mapRegion.GetComponent < RegionClick >().GetDefColor();
    }
}
