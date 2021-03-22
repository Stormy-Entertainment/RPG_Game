using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequence : MonoBehaviour
{
    public GameObject CutsceneCam;
    private Camera mainCam;



    void Start()
    {
        mainCam = Camera.main;
        StartCoroutine(TheSequence());
    }

    IEnumerator TheSequence()
    {
        mainCam.gameObject.SetActive(false);
        yield return new WaitForSeconds(6);
        CutsceneCam.SetActive(false);
        mainCam.gameObject.SetActive(true);
    }
}
