using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstepper : MonoBehaviour
{
    public LayerMask Ground;

    private AudioSource m_AudioSource;

    public AudioClip[] m_footStepSound;
    
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlayFootStep()
    {
        Debug.Log("hello 2");
        int n = Random.Range(1, m_footStepSound.Length);
        m_AudioSource.clip = m_footStepSound[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_footStepSound[n] = m_footStepSound[0];
        m_footStepSound[0] = m_AudioSource.clip;
    }
}
