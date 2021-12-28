using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleLink : MonoBehaviour, Showcasable
{
    [SerializeField]
    private GameObject showcase;
    [SerializeField]
    private GameObject backup;

    public GameObject GetLink()
    {
        return showcase;
    }

    public void SwapLink()
    {
        if(backup != null)
        {
            showcase = backup;
            backup = null;
        }
    }
}
