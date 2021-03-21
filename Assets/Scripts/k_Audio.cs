using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_Audio : MonoBehaviour
{
    public AudioClip skyIsland;
    public AudioClip bossFight;

    public bool boss;

    public AudioSource audio;
    private Transform m_Player;

    void Start()
    {
        boss = false;

        //using player position to descide which audio to play
        m_Player = GameObject.FindWithTag("Player").transform;

        audio = GetComponent<AudioSource>();
        audio.loop = true;

        audio.clip = skyIsland;
        audio.Play();
    }


    //If the player enters the boss arena(check by player position(Z)) , play boss audio clip.
    void Update()
    {
        if (boss == false)
        {
            if (m_Player.transform.position.z >75)
            {
                audio.clip = bossFight;
                audio.Play();
                boss = true;
            }
        } 
    }

}
