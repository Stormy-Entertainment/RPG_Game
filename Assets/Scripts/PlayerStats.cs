using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float m_Health = 100;
    public float m_AttackSpeed = 200; //Range(0 - 1000)
    public float m_MoveSpeed = 60; //Range(0 - 100)

    //Stats
    //public int m_Agility = 20; //Increases Damage & Attack Speed
   // public int m_Intellect = 20; //Attack Speed
   // public int m_Stamina = 20; //Increases MoveSpeed & Damage
   // public int m_Strength = 20; //Increases Health Grade & Armor

    private void Start()
    {
        LoadPlayerStats();
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
        SavePlayerStats();
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
        SavePlayerStats();
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
        SavePlayerStats();
    }

    public void DecreaseHealth(float value)
    {
        m_Health = m_Health - value;
        if (m_Health <= 0f)
        {
            m_Health = 0f;
            GameHandler.instance.DecreaseLives(this.gameObject); ;
        }
        StatsUI.instance.UpdateHealthBar(m_Health);
        SavePlayerStats();
    }

    public void ResetHealth()
    {
        m_Health = 100;
        StatsUI.instance.UpdateHealthBar(m_Health);
    }
    #endregion

    #region //Database

    public void LoadPlayerStats()
    {
        m_Health = PlayerPrefs.GetFloat("PlayerHealth", 100);
        m_AttackSpeed = PlayerPrefs.GetFloat("PlayerAttackSpeed", 200);
        m_MoveSpeed = PlayerPrefs.GetFloat("PlayerMoveSpeed", 60);
    }

    public void SavePlayerStats()
    {
        PlayerPrefs.SetFloat("PlayerHealth", m_Health);
        PlayerPrefs.SetFloat("PlayerAttackSpeed", m_AttackSpeed);
        PlayerPrefs.SetFloat("PlayerMoveSpeed", m_MoveSpeed);
    }
    #endregion

}
