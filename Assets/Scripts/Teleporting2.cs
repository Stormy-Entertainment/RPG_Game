using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleporting2 : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject info;
    public AudioSource audioSource;

    private Transform Player;
    private CharacterController chracterController;
    private Camera mainCamera;
    private bool entered = false;
    public bool targetArenaScene = true;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        chracterController = Player.GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (entered)
        {
            info.transform.rotation = Quaternion.LookRotation(info.transform.position - mainCamera.transform.position);
        }

        if (Input.GetButtonDown("Interact"))
        {
            if (entered)
            {
                StartCoroutine(Teleport());
            }
        }
    }

    IEnumerator Teleport()
    {
        chracterController.enabled = false;
        audioSource.Play();
        yield return new WaitForSeconds(0.02f);
        Player.position = teleportTarget.position;
        Player.rotation = teleportTarget.rotation;
        chracterController.enabled = true;
        entered = false;
        if (targetArenaScene)
        {
            GameHandler.instance.EnableArenaScene();
        }
        else
        {
            GameHandler.instance.LevelSetUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            entered = true;
            info.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            entered = false;
            info.SetActive(false);
        }
    }
}
