using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequence : MonoBehaviour
{
    public GameObject Cam1;
    public GameObject playerCam;

    void Start()
    {
        StartCoroutine(TheSequence());
    }

    IEnumerator TheSequence()
    {
        yield return new WaitForSeconds(6);
        Cam1.SetActive(false);
        playerCam.SetActive(true);
    }



}
