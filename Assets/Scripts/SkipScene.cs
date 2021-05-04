using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipScene : MonoBehaviour
{
    public String sceneName;

    public void playGame()
    {
        StartCoroutine(LoadSceneRoutine());
    }

private IEnumerator LoadSceneRoutine()
{
    GameHandler.instance.ChangeRespawnPoint(sceneName);
    FindObjectOfType<PauseUI>().DialogSceneClosed();
    yield return new WaitForSeconds(0.1f);
    SceneManager.LoadScene(sceneName);
}
}