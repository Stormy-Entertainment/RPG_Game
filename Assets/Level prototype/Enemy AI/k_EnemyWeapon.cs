using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_EnemyWeapon : MonoBehaviour
{
    //Attach this script to the weapon, this object collider will active when the attack animation played 
    public float attackDamage;
    private GameObject player;
    public AudioSource audio;

    void OnTriggerEnter(Collider other)
    {
        player = GameObject.Find("Player");

        if (other.gameObject.tag == "Player")
        {
            audio = GetComponent<AudioSource>();
            GetComponent<AudioSource>().Play();

            player.GetComponent<k_hpCon>().DecreaseHealth(attackDamage);
            Debug.Log("Hit");
        }
    }
}
