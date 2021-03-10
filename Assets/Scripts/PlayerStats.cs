using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float m_Health = 100;

    public void IncreaseHealth(float value)
    {
        m_Health = m_Health + value;
        if(m_Health >= 100f)
        {
            m_Health = 100f;
        }
        StatsUI.instance.UpdateHealthBar(m_Health);
    }

    public void DecreaseHealth(float value)
    {
        m_Health = m_Health - value;
        if (m_Health <= 0f)
        {
            m_Health = 0f;
            Destroy(this.gameObject);
        }
        StatsUI.instance.UpdateHealthBar(m_Health);
    }
}
