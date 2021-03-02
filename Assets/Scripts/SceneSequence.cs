using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequence : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    void Start()
    {
        StartCoroutine(TheSequence());
    }

        IEnumerator TheSequence()
    {
        yield return new WaitForSeconds(4);
        cam1.SetActive(true);
        cam2.SetActive(false);
        yield return new WaitForSeconds(4);
        cam1.SetActive(false);
        cam2.SetActive(true);
    }
        

        }

    

    
   

