using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI instance;

    [SerializeField] private GameObject gameOverMenu;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        GameState.instance.PauseTheGame();
    }

    public void OnMenuBtnClick()
    {
        SceneManager.LoadScene(0);
    }
}
