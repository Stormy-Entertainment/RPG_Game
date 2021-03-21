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

    private Transform Player;
    private CharacterController chracterController;
    private Camera mainCamera;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        chracterController = Player.GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    IEnumerator Teleport()
    {
        chracterController.enabled = false;
        audioSource.Play();
        yield return new WaitForSeconds(0.02f);
        Player.position = teleportTarget.position;
        Player.rotation = teleportTarget.rotation;
        chracterController.enabled = true;
        GameHandler.instance.EnableArenaScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            info.SetActive(true);
            pEffect.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            info.transform.rotation = Quaternion.LookRotation(info.transform.position - mainCamera.transform.position);

            if (Input.GetButtonDown("Interact"))
            {
                StartCoroutine(Teleport());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            info.SetActive(false);
            pEffect.SetActive(false);
        }
    }
}
