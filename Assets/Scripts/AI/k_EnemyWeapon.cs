using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_EnemyWeapon : MonoBehaviour
{
    //Attach this script to the weapon, this object collider will active when the attack animation played 
    public float attackDamage;
    private GameObject player;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            audio.Play();

            player.GetComponent<PlayerStats>().DecreaseHealth(attackDamage);
        }
    }
}
