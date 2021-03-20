﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    public Image[] hearts;

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        GameState.instance.PauseTheGame();
    }

    public void OnMenuBtnClick()
    {
        SceneManager.LoadScene(0);
    }

    public void DisableHeart(int hearthNo)
    {
        Color myColor;
        myColor = hearts[hearthNo].color;
        myColor.a = 0.6f;
        hearts[hearthNo].color = myColor;
    }
}
