using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private void Update()
    {
        // Open and Close Pause Menu
        if (Input.GetButtonDown("Pause"))
        {
            if (!GameState.isGameOver && !GameState.isStageCompleted)
            {
                if (pauseMenu.activeSelf)
                {
                    pauseMenu.SetActive(false);
                    GameState.instance.ResumeTheGame();

                }
                else
                {
                    pauseMenu.SetActive(true);
                    GameState.instance.PauseTheGame();
                }
            }
            else
            {
                if (pauseMenu.activeSelf)
                {
                    pauseMenu.SetActive(false);
                    GameState.instance.ResumeTheGame();
                }
            }
        }
    }

    public void OnResumeBtnClick()
    {
        pauseMenu.SetActive(false);
        GameState.instance.ResumeTheGame();
    }

    public void OnMenuBtnClick()
    {
        SceneManager.LoadScene(0);
    }
}
