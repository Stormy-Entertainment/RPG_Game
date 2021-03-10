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
    }

    private void Update()
    {
        
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
    }

    public void LevelUp()
    {
        currentLevel++;
        lvlText.text = currentLevel.ToString("");
    }

    public void UpdateHealthBar(float newHealth)
    {
        //Fill Health Bar Slider
        healthBarSlider.value = newHealth / 100;
    }
}
