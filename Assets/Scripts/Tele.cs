using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tele : MonoBehaviour
{
    public bool dialogSceneOpen = false;
    public bool dialogSceneClose = false;
    public string levelName;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        if (dialogSceneOpen)
        {
            FindObjectOfType<PauseUI>().DialogSceneOpened();
        }
        if (dialogSceneClose)
        {
            FindObjectOfType<PauseUI>().DialogSceneClosed();
        }
        GameHandler.instance.ChangeRespawnPoint(levelName);
        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadScene(levelName);
    }
}