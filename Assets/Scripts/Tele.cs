using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tele : MonoBehaviour
{
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
        GameHandler.instance.ChangeRespawnPoint(levelName);
        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadScene(levelName);
    }
}