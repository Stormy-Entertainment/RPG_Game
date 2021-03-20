using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMessage : MonoBehaviour
{
    void Update()
    {
        float y = Mathf.PingPong(Time.time, 1);
        transform.position = new Vector3(0, y, 0);
    }
}
