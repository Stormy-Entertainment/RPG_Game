using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float m_Health = 100;
    public float m_XPGains = 15;

    public GameObject BloodParticleEffect;

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
        if (m_Health <= 0f)
        {
            InstantiateBloodParticleEffect();
            StatsUI.instance.SetExperience(m_XPGains);
            EnemyManager.instance.SpawnNewEnemy();
            Destroy(transform.parent.gameObject);
        }
    }

    private void InstantiateBloodParticleEffect()
    {
        Instantiate(BloodParticleEffect, transform.position, Quaternion.identity);
    }
}
