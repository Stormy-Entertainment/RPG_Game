﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float m_Health = 100;
    public float m_XPGains = 15;

    public GameObject BloodParticleEffect;
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
                enemyAI.Death();
                StartCoroutine(DestroyDelay(7f));
            }
            else if(enemyAI2 != null)
            {
                enemyAI2.Death();
                StartCoroutine(DestroyDelay(3f));
            }
            else if (enemyAI3 != null)
            {
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
}
