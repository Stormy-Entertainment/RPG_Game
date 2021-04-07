using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_EnemyAttack : MonoBehaviour
{
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
            Debug.Log("Ha Hit");

            //k_hpCon.hp -= attackDamage;
        }
    }
}
