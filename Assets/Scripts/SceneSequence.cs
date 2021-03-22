using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneSequence : MonoBehaviour
{
    public CinemachineVirtualCamera CutsceneCam;

    void Start()
    {
        StartCoroutine(TheSequence());
    }

    IEnumerator TheSequence()
    {
        yield return new WaitForSeconds(6f);
        CutsceneCam.Priority = 50;
    }
}
