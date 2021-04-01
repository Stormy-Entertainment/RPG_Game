using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    
    private Transform Target;
    public float Speed = 1.5f;
    public int CoinsPerItem = 100;

    Vector3 _velocity = Vector3.zero;
    bool _isFollowing = false;

    private void Start()
    {
        FollowingPlayer();
    }

    private void FollowingPlayer()
    {
        Target = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerTarget>().transform;
        _isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFollowing && Target!=null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref _velocity, Time.deltaTime * Speed);
            //transform.position = Vector3.Lerp(transform.position, Target.position, Time.deltaTime * Speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SFXManager.GetInstance().PlaySound("Jump");
            other.GetComponentInParent<Inventory>().IncreaseCoins(CoinsPerItem);
            Destroy(this.gameObject);
        }
    }
}
