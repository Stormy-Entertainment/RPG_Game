using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    private Transform Player;
    [SerializeField] private CinemachineFreeLook ThirdPersonCamera;

    [SerializeField] private int m_PlayerLives = 3;
    [SerializeField] private Transform m_PlayerRespawnPoint;

    [SerializeField] private GameObject LevelScene;
    [SerializeField] private GameObject ArenaScene;

    GameOverUI gameOver;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        gameOver = FindObjectOfType<GameOverUI>();
    }

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        //LevelSetUp();
    }

    public Transform GetPlayer()
    {
        return Player;
    }
            
    public void DecreaseLives(GameObject Player)
    {     
        m_PlayerLives--;
        gameOver.DisableHeart(m_PlayerLives);
        if (m_PlayerLives > 0)
        {
            //Change Position of Player
            Player.GetComponent<CharacterController>().enabled = false;
            Player.transform.position = m_PlayerRespawnPoint.position;
            Player.GetComponent<PlayerStats>().ResetHealth();
            Player.GetComponent<CharacterController>().enabled = true;
        }
        else
        {
            //GameOver
            gameOver.GameOver();
        }
    }

    public void LevelSetUp()
    {
        LevelScene.SetActive(true);
        ArenaScene.SetActive(false);
    }

    public void EnableArenaScene()
    {
        LevelScene.SetActive(false);
        ArenaScene.SetActive(true);
    }
}
