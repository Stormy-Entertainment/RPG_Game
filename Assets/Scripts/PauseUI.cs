using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject UIElement;
    public bool isPaused = false;
    public bool isMenuActivated = true;

    private void Update()
    {
        // Open and Close Pause Menu
        if (Input.GetButtonDown("Pause"))
        {
            if (!GameState.isGameOver && !GameState.isStageCompleted)
            {
                InventoryUI InventoryUI = FindObjectOfType<InventoryUI>();
                ShopSystem ShopKeeper = FindObjectOfType<ShopSystem>();
                if (!isMenuActivated)
                {
                    if (!InventoryUI.isInventoryOpen && !ShopKeeper.shopOpen)
                    {
                        if (pauseMenu.activeSelf)
                        {
                            pauseMenu.SetActive(false);
                            isPaused = false;
                            GameState.instance.ResumeTheGame();

                        }
                        else
                        {
                            pauseMenu.SetActive(true);
                            isPaused = true;
                            GameState.instance.PauseTheGame();
                        }
                    }
                    else if (InventoryUI.isInventoryOpen)
                    {
                        InventoryUI.inventoryUI.SetActive(false);
                        GameState.instance.ResumeTheGame();
                        InventoryUI.isInventoryOpen = false;
                    }
                    else if (ShopKeeper.shopOpen)
                    {
                        ShopKeeper.CloseShop();
                        GameState.instance.ResumeTheGame();
                        ShopKeeper.shopOpen = false;
                    }
                }
            }
        }
    }

    public void OnResumeBtnClick()
    {
        pauseMenu.SetActive(false);
        GameState.instance.ResumeTheGame();
        isMenuActivated = false;
    }

    public void ResumeFromMenu()
    {
        pauseMenu.SetActive(false);
        GameState.instance.ResumeTheGame();
        isMenuActivated = false;
        ActivateUIElement();
    }

    public void OnMenuBtnClick()
    {
        StartCoroutine(PauseMenuRoutine());
    }

    IEnumerator PauseMenuRoutine()
    {
        pauseMenu.SetActive(false);
        UIElement.SetActive(false);
        isPaused = false;
        isMenuActivated = true;
        GameState.instance.ResumeMenu();
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(0);
    }

    public void DisableUIElement()
    {
        UIElement.SetActive(false);
        isMenuActivated = true;
    }

    public void ActivateUIElement()
    {
        UIElement.SetActive(true);
    }

    public void DialogSceneOpened()
    {
        UIElement.SetActive(false);
        isMenuActivated = false;
    }

    public void DialogSceneClosed()
    {
        UIElement.SetActive(true);
        isMenuActivated = false;
    }
}
