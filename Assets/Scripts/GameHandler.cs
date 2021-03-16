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
    [SerializeField] private GameObject m_PlayerPrefab;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        Player = GameObject.FindWithTag("Player").transform;
    }

    public Transform GetPlayer()
    {
        return Player;
    }
            
    public void DecreaseLives()
    {
        m_PlayerLives--;
        if(m_PlayerLives >= 0)
        {
            //Respawn
            GameObject player = Instantiate(m_PlayerPrefab, m_PlayerRespawnPoint.transform.position, m_PlayerRespawnPoint.rotation);
            Player = player.transform;
            ThirdPersonCamera.LookAt = player.transform;
            ThirdPersonCamera.Follow = player.transform;
        }
        else
        {
            //GameOver
            GameOverUI.instance.GameOver();
        }
    }

}
