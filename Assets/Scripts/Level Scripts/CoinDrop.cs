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

    public float playerFollowRadius = 2f;
    public LayerMask playerLayerMask;
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (_isFollowing && Target != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref _velocity, Time.deltaTime * Speed);
            //transform.position = Vector3.Lerp(transform.position, Target.position, Time.deltaTime * Speed);
        }
    }

    private void FixedUpdate()
    {
        CheckPlayerCollision();
    }

    private void FollowingPlayer()
    {
        if (!_isFollowing)
        {
            Target = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerTarget>().transform;
            _isFollowing = true;
        }
    }

    private void CheckPlayerCollision()
    {
        if (Physics.SphereCast(transform.position, playerFollowRadius, Vector3.up, out hit, 20f, playerLayerMask))
        {
            //Debug.Log(hit.collider.gameObject.name);
            //using Sphere Cast for Collison Check
            if (hit.collider.gameObject.tag == "Player")
            {
                FollowingPlayer();
            }
        }
        else
        {
            _isFollowing = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SFXManager.GetInstance().PlaySound("CoinPickUp");
            other.GetComponentInParent<Inventory>().IncreaseCoins(CoinsPerItem);
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerFollowRadius);
    }
}
