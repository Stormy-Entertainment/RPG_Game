﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    public static bool isPaused = false;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    private void Start()
    {
        ResumeTheGame();
    }

    public void PauseTheGame()
    {
        Time.timeScale = 0f;
        EnableCorsor();
        isPaused = true;
    }

    public void ResumeTheGame()
    {
        Time.timeScale = 1f;
        DisableCorsor();
        isPaused = false;
    }

    private void EnableCorsor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void DisableCorsor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
