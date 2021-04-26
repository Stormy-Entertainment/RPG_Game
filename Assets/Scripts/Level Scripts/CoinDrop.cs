using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    private Transform Target;
    [SerializeField] private float Speed = 10f;
    [SerializeField] private float exponentialMultiplier = 0.2f;
    [SerializeField] private int CoinsPerItem = 100;

    bool _isFollowing = false;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_isFollowing && Target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Time.deltaTime * Speed + exponentialMultiplier);
        }
    }

    public void FollowPlayer()
    {
        if (!_isFollowing)
        {
            Target = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerTarget>().transform;
            _isFollowing = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            SFXManager.GetInstance().PlaySound("CoinPickUp");
            other.gameObject.GetComponentInParent<Inventory>().IncreaseCoins(CoinsPerItem);
            Destroy(this.gameObject);
        }
    }
}
