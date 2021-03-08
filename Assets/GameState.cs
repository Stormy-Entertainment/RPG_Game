using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public void PauseTheGame()
    {
        Time.timeScale = 0f;
        EnableCorsor();
    }

    public void ResumeTheGame()
    {
        Time.timeScale = 1f;
        DisableCorsor();
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
