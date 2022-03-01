using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseMenu : MonoBehaviour
{
    public List<SCDisplaySlot> places = new List<SCDisplaySlot>();


    public int PlacesCount()
    {
        return places.Count;
    }


    void Awake()
    {
        SetChildren();
    }

    private void SetChildren()
    {
        foreach (Transform child in transform)
        {
            places.Add( child.gameObject.GetComponent<SCDisplaySlot>() );
        }
    }


    // Adjusting display

    public void DisplaySlot(StructureSlot origin, int i)
    {
        SCDisplaySlot slot = places[i];

        slot.RemoveDisplay();

        slot.SetDisplay(origin);
    }

    public void ClearDisplay()
    {
        foreach(SCDisplaySlot slot in places)
        {
            slot.RemoveDisplay();
        }
    }
}
