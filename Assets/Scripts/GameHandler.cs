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
            Player.transform.position = m_PlayerRespawnPoint.position;
            Player.GetComponent<PlayerStats>().ResetHealth();
        }
        else
        {
            //GameOver
            gameOver.GameOver();
        }
    }

}
