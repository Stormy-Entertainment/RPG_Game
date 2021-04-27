using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_EnemyHead : MonoBehaviour
{
    private Transform player;


    void OnTriggerEnter(Collider other)
    {

        player = GameHandler.instance.GetPlayer();

        if (other.gameObject.tag == "Player")
        {
            player.transform.position += transform.forward * 10;
        }
    }

}
