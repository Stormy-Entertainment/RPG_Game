using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour {

    public GameObject[] gos;
  

	// Use this for initialization
	void Start () {
	    	
	}

    public void SetAnimTrigger(string trigger)
    {
        foreach(GameObject go in gos)
        {
            Animator anim = go.GetComponent<Animator>();
            anim.SetTrigger(trigger);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
