using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequence : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    public GameObject Playercam;
    public GameObject mainCam;
    void Start()
    {
        StartCoroutine(TheSequence());
    }

        IEnumerator TheSequence()
    {
        yield return new WaitForSeconds(1);
        cam1.SetActive(true);
        cam2.SetActive(false);
        yield return new WaitForSeconds(2);
        cam1.SetActive(false);
        cam2.SetActive(true);
        yield return new WaitForSeconds(4);
        Playercam.SetActive(true);
        mainCam.SetActive(true);
    }
        

        }

    

    
   

