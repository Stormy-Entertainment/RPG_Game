using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public static bool isPaused = false;

    private void Update()
    {
        // Open and Close Pause Menu
        if (Input.GetButtonDown("Pause"))
        {
            if (!InventoryUI.isInventoryOpen)
            {
                if (pauseMenu.activeSelf)
                {
                    pauseMenu.SetActive(false);
                    GameState.instance.ResumeTheGame();
                    isPaused = false;
                }
                else
                {
                    pauseMenu.SetActive(true);
                    GameState.instance.PauseTheGame();
                    isPaused = true;
                }
            }
        }
    }

    public void OnResumeBtnClick()
    {
        pauseMenu.SetActive(false);
        GameState.instance.ResumeTheGame();
        isPaused = false;
    }

    public void OnQuitBtnClick()
    {
        Application.Quit();
    }
}
