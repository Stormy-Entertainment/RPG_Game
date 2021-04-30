using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonCamData : MonoBehaviour
{
    private CinemachineFreeLook cinemachineCam;
    public float XSpeed = 200;
    public float YSpeed = 2;

    private void Start()
    {
        cinemachineCam = GetComponent<CinemachineFreeLook>();
        LoadCameraDetails();
        //Load Camera Details on Start up
    }

    private void LoadCameraDetails()
    {
        XSpeed = PlayerPrefs.GetFloat("CamXSpeed", 200);
        YSpeed = PlayerPrefs.GetFloat("CamYSpeed", 2);
        //Loading Datas

        cinemachineCam.m_XAxis.m_MaxSpeed = XSpeed;
        cinemachineCam.m_YAxis.m_MaxSpeed = YSpeed;
        //Applying Changes
    }
}
