using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_EnemyWeapon : MonoBehaviour
{
    //Attach this script to the weapon, this object collider will active when the attack animation played 
    public float attackDamage;
    private Transform player;
    public AudioSource audio;

    void OnTriggerEnter(Collider other)
    {
        player = GameHandler.instance.GetPlayer();

        if (other.gameObject.tag == "Player")
        {
            audio = GetComponent<AudioSource>();
            GetComponent<AudioSource>().Play();

            player.GetComponent<PlayerStats>().DecreaseHealth(attackDamage);
            Debug.Log("Hit");
        }
    }
}
