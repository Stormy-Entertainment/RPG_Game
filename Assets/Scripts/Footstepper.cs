using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstepper : MonoBehaviour
{
    public LayerMask Ground;

    private AudioSource m_AudioSource;

    public AudioClip[] m_stoneFootSounds;
    



    private void Start(){
        m_AudioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other){
        Debug.Log("hello 1");
        if (other.gameObject.layer == Ground)
        {
            Debug.Log("hello 2");
            int n = Random.Range(1, m_stoneFootSounds.Length);
            m_AudioSource.clip = m_stoneFootSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_stoneFootSounds[n] = m_stoneFootSounds[0];
            m_stoneFootSounds[0] = m_AudioSource.clip;
        }
        
    }

}
