using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;

    public float m_Health = 100;
    public float m_XPGains = 15;

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
            StatsUI.instance.SetExperience(m_XPGains);
            Destroy(transform.parent.gameObject);
            OnEnemyKilled();
        }

        //if(OnEnemyKilled != null)
        //{
            //OnEnemyKilled();
        //}
    }
}
