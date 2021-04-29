using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public bool isPaused = false;

    private void Update()
    {
        // Open and Close Pause Menu
        if (Input.GetButtonDown("Pause"))
        {
            if (!GameState.isGameOver && !GameState.isStageCompleted)
            {
                InventoryUI InventoryUI = FindObjectOfType<InventoryUI>();
                ShopSystem ShopKeeper = FindObjectOfType<ShopSystem>();
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
                else if(InventoryUI.isInventoryOpen)
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
