using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public static StatsUI instance;

    public int currentLevel = 1;
    public float experience { get; private set; }

    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private Slider expBarSlider;
    [SerializeField] private Slider healthBarSlider;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        LoadStatsData();
    }

    public static int ExpNeedToLvlUp(int currentLevel)
    {
        if (currentLevel == 0)
            return 0;

        return (currentLevel * currentLevel + currentLevel) * 5;
    }

    public void SetExperience(float exp)
    {
        experience += exp;

        float expNeeded = ExpNeedToLvlUp(currentLevel);
        float previousExperience = ExpNeedToLvlUp(currentLevel - 1);

        //Level up with Exp
        if (experience >= expNeeded)
        {
            LevelUp();
            expNeeded = ExpNeedToLvlUp(currentLevel);
            previousExperience = ExpNeedToLvlUp(currentLevel - 1);
        }

        //Fill Exp Bar Slider with Exp
        expBarSlider.value = (experience - previousExperience) / (expNeeded - previousExperience);

        //Reset the Fillbar
        if (expBarSlider.value == 1)
        {
            expBarSlider.value = 0;
        }
        SaveStatsData();
    }

    public void LevelUp()
    {
        currentLevel++;
        lvlText.text = currentLevel.ToString("");
        if(currentLevel == 5)
        {
            EnemyManager.instance.SpawnNewEnemy();
        }
        else if(currentLevel == 8)
        {
            EnemyManager.instance.SpawnNewEnemy();
        }
        else if (currentLevel == 12)
        {
            EnemyManager.instance.SpawnNewEnemy();
        }
        else if (currentLevel == 20)
        {
            EnemyManager.instance.SpawnNewEnemy();
        }
        else if (currentLevel == 30)
        {
            EnemyManager.instance.SpawnNewEnemy();
        }
        UpdatePlayerStats();
    }

    private void UpdatePlayerStats()
    {
        PlayerStats playerStat = FindObjectOfType<PlayerStats>();
        playerStat.IncreaseAttackSpeed(50);
        SaveStatsData();
    }

    public void UpdateHealthBar(float newHealth)
    {
        //Fill Health Bar Slider
        healthBarSlider.value = newHealth / 100;
    }

    #region //Database
    public void LoadStatsData()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        experience = PlayerPrefs.GetFloat("CurrentExperience", 0);
    }

    public void SaveStatsData()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetFloat("CurrentExperience", experience);
    }
    #endregion
}
