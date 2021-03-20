using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    void Update()
    {
        transform.Translate(2*Time.deltaTime, 0, 0);

        if (this.transform.position.x >64)
        {
            transform.position = new Vector3(-60, transform.position.y, transform.position.z);
        }
        
    }
}
