using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataResetter : MonoBehaviour
{
    private static DataResetter instance;

    private void Awake()
    {
        instance = this;
        int dataResetterCount = FindObjectsOfType<DataResetter>().Length;
        if (dataResetterCount > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        ResetData();
    }

    public void ResetData()
    {
        PlayerPrefs.SetInt("CurrentLevel", 1);
        PlayerPrefs.SetFloat("CurrentExperience", 0);
        PlayerPrefs.SetInt("CurrentMoney", 500);

        PlayerPrefs.SetInt("PlayerHealth", 100);
        PlayerPrefs.SetInt("PlayerAttackSpeed", 200);
        PlayerPrefs.SetInt("PlayerMoveSpeed", 60);
    }
}
