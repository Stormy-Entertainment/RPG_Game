using System.Collections;
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
        GameState.isGameOver = true;
        gameOverMenu.SetActive(true);
        GameState.instance.PauseTheGame();
    }

    public void OnMenuBtnClick()
    {
        StartCoroutine(GameOverMenuRoutine());
    }

    IEnumerator GameOverMenuRoutine()
    {
        gameOverMenu.SetActive(false);
        FindObjectOfType<PauseUI>().DisableUIElement();
        GameState.instance.ResumeMenu();
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(0);
    }

    public void DisableHeart(int heartNo)
    {
        //Color myColor;
        //myColor = hearts[hearthNo].color;
        // myColor.a = 0.6f;
        //hearts[hearthNo].color = myColor;
        hearts[heartNo].gameObject.SetActive(false);
    }
}
