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

<<<<<<< HEAD
    // Update is called once per frame
    void Update()
    {
        if (_isFollowing && Target != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref _velocity, Time.deltaTime * Speed);
        }
    }

    public void FollowPlayer()
=======
    private void Start()
    {
        FollowingPlayer();
    }

    private void FollowingPlayer()
>>>>>>> 46137604eb50a84866dcefbfe5cbe039e4d18613
    {
        Target = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerTarget>().transform;
        _isFollowing = true;
    }

<<<<<<< HEAD
    private void OnCollisionEnter(Collision other)
=======
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
>>>>>>> 46137604eb50a84866dcefbfe5cbe039e4d18613
    {
        if (other.gameObject.tag == "Player")
        {
            SFXManager.GetInstance().PlaySound("CoinPickUp");
            other.gameObject.GetComponentInParent<Inventory>().IncreaseCoins(CoinsPerItem);
            Destroy(this.gameObject);
        }
    }
}
