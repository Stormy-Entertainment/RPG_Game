using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneHandler : MonoBehaviour
{
    [SerializeField] private  GameObject MainMenu;
    [SerializeField] private GameObject OptionsMenu;

    private void Awake()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnPlayButtonClick()
    {
        FindObjectOfType<PauseUI>().ActivateUIElement();
        SceneManager.LoadScene(1);
    }

    public void OnOptionButtonClick()
    {
        OptionsMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void OnBackButtonClick()
    {
        OptionsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
