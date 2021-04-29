using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSensitivity : MonoBehaviour
{
    private Slider slider;
    public bool applyToX = false;
    public bool applyToY = false;

    private void Start()
    {
        slider = GetComponent<Slider>();
        LoadData();
    }

    private void LoadData()
    {
        float XSen = PlayerPrefs.GetFloat("CamXSpeed", 220);
        float YSen = PlayerPrefs.GetFloat("CamYSpeed", 2);
        if (applyToX)
        {
            slider.value = XSen;
        }
        if (applyToY)
        {
            slider.value = YSen;
        }
        //slider.value = FullScreenMovieScalingMode;
    }

    public void SetXSensitivity(float value)
    {
        PlayerPrefs.SetFloat("CamXSpeed", value);
    }

    public void SetYSensitivity(float value)
    {
        PlayerPrefs.SetFloat("CamYSpeed", value);
    }
}
