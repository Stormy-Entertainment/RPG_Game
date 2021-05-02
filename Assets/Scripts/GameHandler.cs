using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    private Transform Player;

    [SerializeField] private int m_PlayerLives = 3;
    [SerializeField] private Transform m_PlayerRespawnPoint;

    [SerializeField] private Transform MainIslandRespawnPoint;
    [SerializeField] private Transform BonusLevelRespawnPoint;
    [SerializeField] private Transform FinalBossBattleRespawnPoint;
    [SerializeField] private Transform CaveRespawnPoint;

    GameOverUI gameOver;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public Transform GetPlayer()
    {
        Player = GameObject.FindWithTag("Player").transform;
        return Player;
    }
            
    public void DecreaseLives(GameObject Player)
    {
        gameOver = FindObjectOfType<GameOverUI>();
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

    public void ChangeRespawnPoint(string Name)
    {
        switch (Name)
        {
            case "MainIsland":
                m_PlayerRespawnPoint = MainIslandRespawnPoint;
                break;
            case "Bonus level":
                m_PlayerRespawnPoint = BonusLevelRespawnPoint;
                break;
            case "Final Boss Battle":
                m_PlayerRespawnPoint = FinalBossBattleRespawnPoint;
                break;
            case "Cave":
                m_PlayerRespawnPoint = CaveRespawnPoint;
                break;
        }
    }
}
