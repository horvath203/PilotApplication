using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueEntry : MonoBehaviour
{
    [SerializeField]
    FYPData panel;

    [SerializeField]
    private int index;

    public void EnterValue(string value)
    {
        Debug.Log(value);
        panel.Entry(index, value);
    }
}
