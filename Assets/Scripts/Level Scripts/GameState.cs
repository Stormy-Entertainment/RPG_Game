using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    public static bool isPaused = false;
    public static bool isGameOver = false;
    public static bool isStageCompleted = false;

    private void Awake()
    {
        instance = this;
        int gameStateCount = FindObjectsOfType<GameState>().Length;
        if (gameStateCount > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
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
