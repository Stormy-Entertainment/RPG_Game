using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Teleporting2 : MonoBehaviour
{
   // public Transform teleportTarget;
    public string TeleportToSceneName;
    public GameObject info;
    public AudioSource audioSource;

    private Camera mainCamera;
    private bool entered = false;
    public bool targetArenaScene = true;

    private void Start()
    {
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

    public void TeleportPlayerToMain()
    {
        StartCoroutine(Teleport());
    }

    IEnumerator Teleport()
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.02f);
        SceneManager.LoadScene(TeleportToSceneName);
        entered = false;
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
