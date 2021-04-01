using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float m_Health = 100;
    public float m_XPGains = 15;

    public GameObject BloodParticleEffect;
    public GameObject Coin;
    private bool Dead = false;

    private void Start()
    {
        GetComponentInChildren<EnemyHealthSlider>().ChangeMaxHealth(m_Health);
    }

    public void IncreaseHealth(float value)
    {
        m_Health = m_Health + value;
        if (m_Health >= 100f)
        {
            m_Health = 100f;
        }
    }

    public void DecreaseHealth(float value)
    {
        m_Health = m_Health - value;
        GetComponentInChildren<EnemyHealthSlider>().UpdateHealthSlider(m_Health);
        if (m_Health <= 0f && !Dead)
        {
            Dead = true;
            StatsUI.instance.SetExperience(m_XPGains);
            EnemyManager.instance.SpawnNewEnemy();
            EnemyAI3 enemyAI3 = GetComponentInParent<EnemyAI3>();
            EnemyAI2 enemyAI2 = GetComponentInParent<EnemyAI2>();
            EnemyAI enemyAI = GetComponentInParent<EnemyAI>();
            InstantiateBloodParticleEffect();
            
            if (enemyAI != null)
            {
                InstantiateCoin(20);
                enemyAI.Death();
                StartCoroutine(DestroyDelay(7f));
            }
            else if(enemyAI2 != null)
            {
                int RandomCoin = Random.Range(1, 4);
                InstantiateCoin(RandomCoin);
                enemyAI2.Death();
                StartCoroutine(DestroyDelay(3f));
            }
            else if (enemyAI3 != null)
            {
                int RandomCoin = Random.Range(1, 4);
                InstantiateCoin(RandomCoin);
                enemyAI3.Death();
                StartCoroutine(DestroyDelay(3f));
            } 
        }
        else
        {
            InstantiateBloodParticleEffect();
            EnemyAI2 enemy = GetComponentInParent<EnemyAI2>();
            if (enemy != null)
            {
                enemy.Hit();
            }
        }
        //Enemy Dead
    }

    IEnumerator DestroyDelay(float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);
        Destroy(transform.parent.gameObject);
    }

    private void InstantiateBloodParticleEffect()
    {
        Instantiate(BloodParticleEffect, transform.position, Quaternion.identity);
    }

    private void InstantiateCoin(int NoOfCoins)
    {
        for (int i = 0; i < NoOfCoins; i++)
        {
            float RandomXPosition = Random.Range(transform.position.x - 2f, transform.position.x + 2f);
            float RandomYPosition = Random.Range(transform.position.y - 2f, transform.position.y + 2f);
            Instantiate(Coin, new Vector3(RandomXPosition, RandomYPosition, transform.position.z), Quaternion.identity);
        }
    }
}
