using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//if this stays empty, replace whole class with local list in regdetails class
public class BuildingMenu : MonoBehaviour
{
    public List<GameObject> buildButtons;



    //this does not work because it starts off disables, so OnStart wont trigger, and OnEnable would call it every time bmenu activates. Awake doesnt work either
    private void ListButtons()
    {
        buildButtons = new List<GameObject>();

        foreach (Transform child in transform)
        {
            buildButtons.Add(child.gameObject);
        }
    }
}