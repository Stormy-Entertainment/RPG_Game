using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballHandler : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject FireBall;
    [SerializeField] private float fireBallSpeed;
    [SerializeField] private float fireBallLifeTime;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform fireBallSpawnPoint;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootFireBall();
        }
    }

    private void ShootFireBall()
    {
        anim.SetTrigger("FireballShoot");
        GameObject mbullet = Instantiate(FireBall);
        //Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawnPoint.parent.GetComponent<Collider>());
        mbullet.transform.position = new Vector3(fireBallSpawnPoint.position.x, fireBallSpawnPoint.position.y, fireBallSpawnPoint.position.z);
        Quaternion playerRotation = this.transform.rotation;
        mbullet.transform.rotation = playerRotation;
        mbullet.GetComponent<Rigidbody>().AddForce(cameraTransform.forward * fireBallSpeed, ForceMode.Impulse);

        StartCoroutine(DestroyBulletTimer(mbullet));
    }

    IEnumerator DestroyBulletTimer(GameObject bullet)
    {
        yield return new WaitForSeconds(fireBallLifeTime);
        if (bullet != null)
        {
            Destroy(bullet);
        }
    }
}
