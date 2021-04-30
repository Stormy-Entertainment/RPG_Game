using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int m_Health = 100;
    public int m_AttackSpeed = 200; //Range(0 - 1000)
    public int m_MoveSpeed = 10; //Range(0 - 100)
    public int m_Armor = 5; //Range(0 - 100)

    private int ArmorMultiplier = 0;

    private void Start()
    {
        LoadPlayerStats();
        StatsUI.instance.UpdateHealthBar(m_Health);
        GetComponent<ThirdPersonMovement>().UpdateMoveMultiplier(m_MoveSpeed);
        UpdateArmorMultiplier(m_Armor);
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
        GetComponent<ThirdPersonMovement>().UpdateMoveMultiplier(m_MoveSpeed);
        FindObjectOfType<PlayerStatsUI>().UpdateText();
        SavePlayerStats();
    }

    public void DecreaseMoveSpeed(int value)
    {
        m_MoveSpeed -= value;
        if (m_MoveSpeed <= 0)
        {
            m_MoveSpeed = 0;
        }
        GetComponent<ThirdPersonMovement>().UpdateMoveMultiplier(m_MoveSpeed);
        FindObjectOfType<PlayerStatsUI>().UpdateText();
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
        FindObjectOfType<PlayerStatsUI>().UpdateText();
        SavePlayerStats();
    }

    public void DecreaseAttackSpeed(int value)
    {
        m_AttackSpeed -= value;
        if (m_AttackSpeed <= 0)
        {
            m_AttackSpeed = 0;
        }
        FindObjectOfType<PlayerStatsUI>().UpdateText();
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
        UpdateArmorMultiplier(m_Armor);
        FindObjectOfType<PlayerStatsUI>().UpdateText();
        SavePlayerStats();
    }

    public void DecreaseArmor(int value)
    {
        m_Armor -= value;
        if (m_Armor <= 0)
        {
            m_Armor = 0;
        }
        UpdateArmorMultiplier(m_Armor);
        FindObjectOfType<PlayerStatsUI>().UpdateText();
        SavePlayerStats();
    }

    public void UpdateArmorMultiplier(int value)
    {
        float normalizedValue = Mathf.InverseLerp(0, 100, value);
        ArmorMultiplier = Mathf.RoundToInt(Mathf.Lerp(0, 10, normalizedValue));
    }
    #endregion

    #region //Health
    public void IncreaseHealth(int value)
    {
        m_Health = m_Health + value + ArmorMultiplier;
        if(m_Health >= 100)
        {
            m_Health = 100;
        }
        FindObjectOfType<PlayerStatsUI>().UpdateText();
        StatsUI.instance.UpdateHealthBar(m_Health);
    }

    public void DecreaseHealth(int value)
    {
        m_Health = m_Health - (value + ArmorMultiplier);
        if (m_Health <= 0)
        {
            m_Health = 0;
            GameHandler.instance.DecreaseLives(this.gameObject); ;
        }
        FindObjectOfType<PlayerStatsUI>().UpdateText();
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
        m_MoveSpeed = PlayerPrefs.GetInt("PlayerMoveSpeed", 10);
        m_Armor = PlayerPrefs.GetInt("PlayerArmor", 5);
        FindObjectOfType<PlayerStatsUI>().UpdateText();
    }

    public void SavePlayerStats()
    {
        PlayerPrefs.SetInt("PlayerHealth", m_Health);
        PlayerPrefs.SetInt("PlayerAttackSpeed", m_AttackSpeed);
        PlayerPrefs.SetInt("PlayerMoveSpeed", m_MoveSpeed);
        PlayerPrefs.SetInt("PlayerArmor", m_Armor);
    }
    #endregion

}
