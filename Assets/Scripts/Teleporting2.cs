using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleporting2 : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject thePlayer;
    public GameObject info;
    public GameObject pEffect;
    public AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {

        if (other == thePlayer)
        {
            Debug.Log("stay");
            info.SetActive(true);
            info.transform.LookAt(Camera.main.transform);
            pEffect.SetActive(true);

            if (Input.GetKey(KeyCode.P))
            {
                thePlayer.transform.position = teleportTarget.transform.position;
                audioSource.Play();
                print("go");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other == thePlayer)
        {
            print("stay");
            info.SetActive(false);
            pEffect.SetActive(false);
            info.transform.LookAt(Camera.main.transform);
        }

    }
}
