using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneHandler : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnOptionButtonClick()
    {

    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
