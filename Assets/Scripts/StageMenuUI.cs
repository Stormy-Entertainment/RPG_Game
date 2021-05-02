using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject stageMenu;

    public void LevelCompleted()
    {
        GameState.isStageCompleted = true;
        stageMenu.SetActive(true);
        GameState.instance.PauseTheGame();
    }

    public void OnContinueBtnClick()
    {
        GameState.isStageCompleted = false;
        GameState.instance.ResumeTheGame();
        stageMenu.SetActive(false);
    }
}
