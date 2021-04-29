using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public delegate void OnPlayerStatsChanged();
    public OnPlayerStatsChanged onPlayerStatsChangedCallback;

    public int m_Health = 100;
    public int m_AttackSpeed = 200; //Range(0 - 1000)
    public int m_MoveSpeed = 60; //Range(0 - 100)
    public int m_Armor = 60; //Range(0 - 100)

    //Stats
    //public int m_Agility = 20; //Increases Damage & Attack Speed
    // public int m_Intellect = 20; //Attack Speed
    // public int m_Stamina = 20; //Increases MoveSpeed & Damage
    // public int m_Strength = 20; //Increases Health Grade & Armor

    private void Start()
    {
        LoadPlayerStats();
        StatsUI.instance.UpdateHealthBar(m_Health);
        //ResetData();
    }

    #region //Movement Speed
    public float GetMovementSpeed()
    {
        return m_MoveSpeed;
    }

    public void IncreaseMoveSpeed(int value)
    {
        m_MoveSpeed += value;
        if (m_MoveSpeed >= 100)
        {
            m_MoveSpeed = 100;
        }
        if (onPlayerStatsChangedCallback != null)
            onPlayerStatsChangedCallback.Invoke();
        SavePlayerStats();
    }

    public void DecreaseMoveSpeed(int value)
    {
        m_MoveSpeed -= value;
        if (m_MoveSpeed <= 0)
        {
            m_MoveSpeed = 0;
        }
        if (onPlayerStatsChangedCallback != null)
            onPlayerStatsChangedCallback.Invoke();
        SavePlayerStats();
    }
    #endregion

    #region //Attack Speed
    public float GetAttactSpeed()
    {
        return m_AttackSpeed;
    }

    public void IncreaseAttackSpeed(int value)
    {
        m_AttackSpeed += value;
        if(m_AttackSpeed >= 1000)
        {
            m_AttackSpeed = 1000;
        }
        if (onPlayerStatsChangedCallback != null)
            onPlayerStatsChangedCallback.Invoke();
        SavePlayerStats();
    }

    public void DecreaseAttackSpeed(int value)
    {
        m_AttackSpeed -= value;
        if (m_AttackSpeed <= 0)
        {
            m_AttackSpeed = 0;
        }
        if (onPlayerStatsChangedCallback != null)
            onPlayerStatsChangedCallback.Invoke();
        SavePlayerStats();
    }
    #endregion

    #region //Armor
    public float GetArmor()
    {
        return m_Armor;
    }

    public void IncreaseArmor(int value)
    {
        m_Armor += value;
        if (m_Armor >= 100)
        {
            m_Armor = 100;
        }
        if (onPlayerStatsChangedCallback != null)
            onPlayerStatsChangedCallback.Invoke();
        SavePlayerStats();
    }

    public void DecreaseArmor(int value)
    {
        m_Armor -= value;
        if (m_Armor <= 0)
        {
            m_Armor = 0;
        }
        if (onPlayerStatsChangedCallback != null)
            onPlayerStatsChangedCallback.Invoke();
        SavePlayerStats();
    }
    #endregion

    #region //Health
    public void IncreaseHealth(int value)
    {
        m_Health = m_Health + value;
        if(m_Health >= 100)
        {
            m_Health = 100;
        }
        if (onPlayerStatsChangedCallback != null)
            onPlayerStatsChangedCallback.Invoke();
        StatsUI.instance.UpdateHealthBar(m_Health);
    }

    public void DecreaseHealth(int value)
    {
        m_Health = m_Health - value;
        if (m_Health <= 0)
        {
            m_Health = 0;
            GameHandler.instance.DecreaseLives(this.gameObject); ;
        }
        if (onPlayerStatsChangedCallback != null)
            onPlayerStatsChangedCallback.Invoke();
        StatsUI.instance.UpdateHealthBar(m_Health);
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
        m_Health = PlayerPrefs.GetInt("PlayerHealth", 100);
        m_AttackSpeed = PlayerPrefs.GetInt("PlayerAttackSpeed", 200);
        m_MoveSpeed = PlayerPrefs.GetInt("PlayerMoveSpeed", 60);
        if (onPlayerStatsChangedCallback != null)
            onPlayerStatsChangedCallback.Invoke();
    }

    public void SavePlayerStats()
    {
        PlayerPrefs.SetInt("PlayerHealth", m_Health);
        PlayerPrefs.SetInt("PlayerAttackSpeed", m_AttackSpeed);
        PlayerPrefs.SetInt("PlayerMoveSpeed", m_MoveSpeed);
    }
    #endregion

}
