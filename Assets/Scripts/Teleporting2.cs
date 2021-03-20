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
    public Camera mainCamera;


    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            print("stay");
            info.SetActive(true);
            info.transform.rotation = Quaternion.LookRotation(info.transform.position - mainCamera.transform.position);
            pEffect.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                thePlayer.GetComponent<CharacterController>().enabled = false;
                thePlayer.transform.position = teleportTarget.transform.position;
                thePlayer.transform.rotation = teleportTarget.transform.rotation;

                thePlayer.GetComponent<CharacterController>().enabled = true;

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
        }

    }
}
