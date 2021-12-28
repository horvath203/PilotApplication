using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class SCDisplaySlot : MonoBehaviour
{
    [SerializeField]
    private StructureSlot slot = null;

    [SerializeField]
    private GameObject displayObject = null;

    public StructureSlot GetStructureSlot()
    {
        return slot;
    }

    public bool IsDisplayed()
    {
        if(displayObject != null)
        {
            return true;
        }
        return false;
    }

    public void SetDisplay(StructureSlot slot)
    {
        this.slot = slot;

        displayObject = Instantiate(slot.GetShowcaseIcon(), transform);
    }

    public void RemoveDisplay()
    {
        if (IsDisplayed())
        {
            Destroy(displayObject);
            slot = null;
        }
    }




    void OnMouseUpAsButton()
    {
        if ( (slot != null) && slot.IsOccupied() ) {
            CountryManager.Instance.CallBuildingDetails(this);
        }
    }
}
