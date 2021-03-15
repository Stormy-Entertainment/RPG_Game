using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal2 : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject thePlayer;
    public GameObject info;
    public GameObject pEffect;
    public AudioSource audioSource;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            print("stay");
            info.SetActive(true);
            info.transform.LookAt(Camera.main.transform);
            pEffect.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                thePlayer.transform.position = teleportTarget.transform.position;
                audioSource.Play();
                print("go");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            print("stay");
            info.SetActive(false);
            pEffect.SetActive(false);
            info.transform.LookAt(Camera.main.transform);
        }

    }
}
