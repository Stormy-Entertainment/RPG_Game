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
            k_Enemy enemyAI = GetComponentInParent<k_Enemy>();
            k_EnemyRangeAtk enemyAI2 = GetComponentInParent<k_EnemyRangeAtk>();
            
            if (enemyAI != null)
            {
                enemyAI.Death();
            }
            else if(enemyAI2 != null)
            {
                enemyAI2.Death();
            }
            int RandomCoin = Random.Range(1, 4);
            InstantiateBloodParticleEffect();
            InstantiateCoin(RandomCoin);
            StartCoroutine(DestroyDelay(3f));
        }
        else
        {
            InstantiateBloodParticleEffect();
            k_Enemy enemy = GetComponentInParent<k_Enemy>();
            k_EnemyRangeAtk enemyAI2 = GetComponentInParent<k_EnemyRangeAtk>();
            if (enemy != null)
            {
                enemy.Hit();
            }
            if (enemyAI2 != null)
            {
                enemyAI2.Hit();
            }
        }
        //Enemy Dead
    }

    IEnumerator DestroyDelay(float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);;
        Destroy(transform.parent.transform.parent.gameObject);
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
            float RandomZPosition = Random.Range(transform.position.z - 2f, transform.position.z + 2f);
            Instantiate(Coin, new Vector3(RandomXPosition, transform.position.y, RandomZPosition), Quaternion.identity);
        }
    }
}
