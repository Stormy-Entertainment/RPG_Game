using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOutline : MonoBehaviour
{

    public Outline myScript;

    void Start()
    {
        myScript = GetComponent<Outline>();
        myScript.enabled = false;

    }

    public void OnMouseOver()
    {
        myScript.enabled = true;
    }
    public void OnMouseExit()
    {
        myScript.enabled = false;
    }


}
