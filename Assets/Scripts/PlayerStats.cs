using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float m_Health = 100;
    public float m_AttackSpeed = 200; //Range(0 - 1000)
    public float m_MoveSpeed = 60; //Range(0 - 100)

    //Stats
    public int m_Agility = 20; //Increases Damage & Attack Speed
    public int m_Intellect = 20; //Attack Speed
    public int m_Stamina = 20; //Increases MoveSpeed & Damage
    public int m_Strength = 20; //Increases Health Grade & Armor

    private void Start()
    {
        StatsUI.instance.UpdateHealthBar(m_Health);
    }

    #region //Movement Speed
    public float GetMovementSpeed()
    {
        return m_MoveSpeed;
    }

    public void IncreaseMoveSpeed(float value)
    {
        m_MoveSpeed += value;
    }
    #endregion

    #region //Attack Speed
    public float GetAttactSpeed()
    {
        return m_AttackSpeed;
    }

    public void IncreaseAttackSpeed(float value)
    {
        m_AttackSpeed += value;
        if(m_AttackSpeed >= 1000)
        {
            m_AttackSpeed = 1000f;
        }
    }
    #endregion

    #region //Health
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
            GameHandler.instance.DecreaseLives();
            StatsUI.instance.UpdateHealthBar(m_Health);
            Destroy(this.gameObject);
        }
        StatsUI.instance.UpdateHealthBar(m_Health);
    }
    #endregion
}
