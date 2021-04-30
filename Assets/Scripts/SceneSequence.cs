using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneSequence : MonoBehaviour
{
    public CinemachineVirtualCamera CutsceneCam;
    public float waitForSec = 6;

    void Start()
    {
        StartCoroutine(TheSequence());
    }

    IEnumerator TheSequence()
    {
        yield return new WaitForSeconds(waitForSec);
        CutsceneCam.Priority = 50;
    }
}
