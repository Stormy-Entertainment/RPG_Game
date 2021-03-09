using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private GameObject target;
    public float fireBallImpulseSpeed = 100f;
    private float moveSpeed = 60f;
    private bool isTargeted = false;

    private void Update()
    {
        if (target != null && isTargeted == true)
        {
            float step = moveSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
    }

    public void TargetedFire(GameObject t)
    {
        target = t;
        isTargeted = true;
    }

    public void ImpulseFire(Transform cameraTrans)
    {
        GetComponent<Rigidbody>().AddForce(cameraTrans.forward * fireBallImpulseSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //Apply damage to enemy helth
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
