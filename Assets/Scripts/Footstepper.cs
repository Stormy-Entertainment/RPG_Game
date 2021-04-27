using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstepper : MonoBehaviour
{

    public float Length;
    private AudioSource m_AudioSource;

    public AudioClip[] m_stoneFootSounds;
    public AudioClip[] m_WoodFootSounds;



    private void Start(){
        m_AudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other){
            if(other.gameObject.CompareTag("Landmark")) {
            int n = Random.Range(1, m_stoneFootSounds.Length);
            m_AudioSource.clip = m_stoneFootSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_stoneFootSounds[n] = m_stoneFootSounds[0];
            m_stoneFootSounds[0] = m_AudioSource.clip;
        }
        if (other.gameObject.CompareTag("Wood"))
        {
            int n = Random.Range(1, m_WoodFootSounds.Length);
            m_AudioSource.clip = m_WoodFootSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_WoodFootSounds[n] = m_WoodFootSounds[0];
            m_WoodFootSounds[0] = m_AudioSource.clip;
        }
    }

}
