using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballHandler : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject FireBall;
    [SerializeField] private float fireBallSpeed;
    [SerializeField] private float fireBallLifeTime;
    [SerializeField] private bool isFiring = false;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform fireBallSpawnPoint;

    private bool m_HitDetect;
    public Vector3 m_HitBoxRadius;
    public float m_HitBoxDistance;
    private Collider m_Collider;
    private RaycastHit m_Hit;
    public LayerMask enemyLayerMask;

    public List<Outline> HighlightedOutline = new List<Outline>();
    private bool HighLighted = false;


    void Start()
    {
        //Choose the distance the Box can reach to
        m_Collider = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !GameState.isPaused)
        {
            if (!isFiring)
            {
                ShootFireBall();
            }
        }
    }

    void FixedUpdate()
    {
        //Test to see if there is a hit using a BoxCast
        //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
        //Also fetch the hit data
        m_HitDetect = Physics.BoxCast(m_Collider.bounds.center,
            m_HitBoxRadius, transform.forward,
            out m_Hit, transform.rotation, m_HitBoxDistance, enemyLayerMask);

            HighLightEnemy();
    }

    private void ShootFireBall()
    {
        isFiring = true;
        anim.SetTrigger("FireballShoot");
        GameObject mbullet = Instantiate(FireBall);
        mbullet.transform.position = new Vector3(fireBallSpawnPoint.position.x, fireBallSpawnPoint.position.y, fireBallSpawnPoint.position.z);
        Quaternion playerRotation = this.transform.rotation;
        mbullet.transform.rotation = playerRotation;

        CheckEnemyAtRange(mbullet);
        SFXManager.GetInstance().PlaySound("FireballSFX");
        StartCoroutine(DestroyBulletTimer(mbullet));
        StartCoroutine(ResetFireball());
    }

    private IEnumerator ResetFireball()
    {
        float OldMax = 1000f;
        float OldMin = 0f;
        float NewMax = 0.1f;
        float NewMin = 1;

        float playerAttactSpeed = GetComponent<PlayerStats>().GetAttactSpeed();

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float fireballLoadingTime = ((((playerAttactSpeed - OldMin) * NewRange) / OldRange) + NewMin);
        yield return new WaitForSeconds(fireballLoadingTime);
        isFiring = false;
    }

    private void HighLightEnemy()
    {
        if (m_HitDetect && m_Hit.collider.tag == "Enemy")
        {
            if (!HighLighted)
            {
                if (HighlightedOutline.Count >= 5)
                {
                    HighLighted = true;
                }
                Outline outline = m_Hit.collider.gameObject.GetComponent<Outline>();
                outline.enabled = true;
                HighlightedOutline.Add(outline);         
            }
        }
        else
        {
            foreach (Outline outline in HighlightedOutline)
            {
                if (outline != null)
                {
                    outline.enabled = false;
                }
            }
                HighLighted = false;
                HighlightedOutline.Clear();
        }
    }

    private void CheckEnemyAtRange(GameObject bul)
    {
        // If raycast hit Enemy tag
        // Assign bullet with target enemy
        //If Raycast hit Target
        if (HighlightedOutline.Count >= 1)
        {
            if (bul != null && HighlightedOutline[0] != null)
            {
                bul.GetComponent<Fireball>().TargetedFire(HighlightedOutline[0].gameObject);
            }
        }
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
        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            Gizmos.color = Color.green;
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, m_HitBoxRadius);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            Gizmos.color = Color.red;
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * m_HitBoxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * m_HitBoxDistance, m_HitBoxRadius);
        }
    }
}
