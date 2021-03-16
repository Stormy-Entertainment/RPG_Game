using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public Transform[] m_SpawnPoints;
    public GameObject m_EnemyPrefab;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public void SpawnNewEnemy()
    {
        int randomNumber = Random.Range(0, m_SpawnPoints.Length);
        Instantiate(m_EnemyPrefab, m_SpawnPoints[randomNumber].transform.position, Quaternion.identity);
    }
}
