using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CurrencyManager : MonoBehaviour
{
    #region Singleton

    public static CurrencyManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of CurrencyManager found!");
            return;
        }
        instance = this;
    }

    #endregion

    public float coinAmount;
    public Text coinText;
    Transform coinUITransform;
    Vector3 originalCoinSize;
    Vector3 originalTextSize;

    
    void Start()
    {
        coinAmount = 0;
        coinText.text = coinAmount.ToString();
        coinUITransform = GameObject.Find("CoinSprite").transform;
        originalCoinSize = coinUITransform.transform.localScale;
        originalTextSize = coinText.transform.localScale;
    }

    public void AddCoin(float value)
    {
        coinAmount += Convert.ToInt32(value);
        UpdateUI();
        Debug.Log("Total Coins: " + coinAmount);
        GameObject.Find("CionsParent").GetComponent<AudioSource>().Play();
    }

    public void RemoveCoin(float value)
    {
        coinAmount -= Convert.ToInt32(value);
        UpdateUI();
        Debug.Log("Total Coins: " + coinAmount);
        GameObject.Find("CionsParent").GetComponent<AudioSource>().Play();
    }


    public void UpdateUI()
    {
        coinText.text = coinAmount.ToString();
    }

    private void ShakeCionUI()
    {
        coinUITransform.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        coinText.transform.localScale = new Vector3(1f, 1.3f, 1f);
        StartCoroutine(TimeBetweenUIChanges());
    }

    IEnumerator TimeBetweenUIChanges()
    {
        yield return new WaitForSeconds(.10f);
        coinUITransform.transform.localScale = originalCoinSize;
        coinText.transform.localScale = originalTextSize;
    }

}
