﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevel (float sliderValue)

    {
        mixer.SetFloat("BGVolume", Mathf.Log10(sliderValue) * 20);
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }
}
