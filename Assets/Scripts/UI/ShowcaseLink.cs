using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseLink : MonoBehaviour, Showcasable
{
    [SerializeField]
    private GameObject showcase;

    public GameObject GetLink()
    {
        return showcase;
    }
}
