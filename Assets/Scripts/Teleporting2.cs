using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleporting2 : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject info;
    public GameObject pEffect;
    public AudioSource audioSource;

    private Transform m_Player;
    private Camera mainCamera;

    private void Start()
    {
        m_Player = GameObject.FindWithTag("Player").transform;
        mainCamera = Camera.main;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            print("stay");
            pEffect.SetActive(true);
            info.SetActive(true);
            //info.transform.rotation = Quaternion.LookRotation(info.transform.position - mainCamera.transform.position);
            

            if (Input.GetButtonDown("Interact"))
            {
                m_Player.GetComponent<CharacterController>().enabled = false;
                m_Player.position = teleportTarget.position;
                m_Player.rotation = teleportTarget.rotation;
                Debug.Log("Interact sucessful");
                m_Player.GetComponent<CharacterController>().enabled = true;

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
