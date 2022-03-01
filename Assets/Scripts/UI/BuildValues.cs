using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildValues : MonoBehaviour
{
    [SerializeField]
    private GameObject undoButton;
    [SerializeField]
    private GameObject restoreButton;


    [SerializeField]
    private Text productions;


    public void SetBuildingDetails(StructureSlot slot)
    {
        undoButton.SetActive(!slot.IsPermanent());

        //check availability of restoration resources first
        restoreButton.SetActive(slot.IsRuined());

        DisplayProductions(slot.GetBuilding());
    }


    private void DisplayProductions(BuildingType building)
    {
        productions.text = building.StringProductions();
    }
}
