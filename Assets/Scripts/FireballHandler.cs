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

    public bool m_HitDetect;
    public float m_MaxDistance;
    Collider m_Collider;
    RaycastHit m_Hit;

    public LayerMask enemyLayerMask;

    void Start()
    {
        //Choose the distance the Box can reach to
        m_Collider = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootFireBall();
        }
    }

    void FixedUpdate()
    {
        //Test to see if there is a hit using a BoxCast
        //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
        //Also fetch the hit data
        m_HitDetect = Physics.BoxCast(m_Collider.bounds.center,
            transform.localScale, transform.forward,
            out m_Hit, transform.rotation, m_MaxDistance, enemyLayerMask);

        if (m_HitDetect)
        {
            //Output the name of the Collider your Box hit
            Debug.Log("Hit : " + m_Hit.collider.name);
        }
    }

    private void ShootFireBall()
    {
        anim.SetTrigger("FireballShoot");
        GameObject mbullet = Instantiate(FireBall);
        mbullet.transform.position = new Vector3(fireBallSpawnPoint.position.x, fireBallSpawnPoint.position.y, fireBallSpawnPoint.position.z);
        Quaternion playerRotation = this.transform.rotation;
        mbullet.transform.rotation = playerRotation;

        //mbullet.GetComponent<Rigidbody>().AddForce(cameraTransform.forward * fireBallSpeed, ForceMode.Impulse);

        CheckEnemyAtRange(mbullet);

        StartCoroutine(DestroyBulletTimer(mbullet));
    }

    private void CheckEnemyAtRange(GameObject bul)
    {
        // If raycast hit Enemy tag
        // Assign bullet with target enemy
        if (m_HitDetect)
        {
            //If Raycast hit Target
            bul.GetComponent<Fireball>().TargetedFire(m_Hit.collider.gameObject);
        }
        else
        {
            bul.GetComponent<Fireball>().ImpulseFire(cameraTransform);
        }
        //Else
        
    }

    IEnumerator DestroyBulletTimer(GameObject bullet)
    {
        yield return new WaitForSeconds(fireBallLifeTime);
        if (bullet != null)
        {
            Destroy(bullet);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * m_MaxDistance, transform.localScale);
        }
    }
}
