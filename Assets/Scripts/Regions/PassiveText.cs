using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveText : MonoBehaviour
{
    [SerializeField]
    TextMesh text;

    void Start()
    {
        //text = GetComponent<TextMesh>();
    }

    public void ChangeText(string newText)
    {
        text.text = "x " + newText;
    }
}
