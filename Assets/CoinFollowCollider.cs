using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFollowCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponentInParent<CoinDrop>().FollowPlayer();
        }
    }
}
