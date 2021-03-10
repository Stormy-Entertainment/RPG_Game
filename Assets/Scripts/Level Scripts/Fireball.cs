using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private GameObject target;
    private Transform player;

    public float fireBallImpulseSpeed = 100f;
    public float Damage = 15f;
    private float moveSpeed = 60f;
    private bool isTargeted = false;
    private bool impulseFired = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (target != null && isTargeted == true)
        {
            float step = moveSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
        else if(!impulseFired)
        {
            ImpulseFire();
        }
    }

    public void TargetedFire(GameObject t)
    {
        target = t;
        isTargeted = true;
    }

    public void ImpulseFire()
    {
        impulseFired = true;
        GetComponent<Rigidbody>().AddForce(player.forward * fireBallImpulseSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            //Apply damage to enemy helth
            other.gameObject.GetComponent<EnemyStats>().DecreaseHealth(Damage);
            SFXManager.GetInstance().PlaySound("FirehitSFX");
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
