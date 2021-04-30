using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_EnemyRangeAtk : MonoBehaviour
{
    private Transform playerPoint;
    private Transform player;

    public Animator animator;
    public UnityEngine.AI.NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;

    //Defence
    public Transform[] defPoint;
    public int defPointNumber;
    private int defPointIndex = 0;
    public float walkSpeed;
    public float chaseSpeed;
    public float defDirection;

    //States
    public float sightRange, attackRange, rangeAttackRange;
    public bool playerInSightRange, playerInAttackRange, playerInRangeAttackRange;
    private bool dead = false;

    //Attacking
    public int m_Damage = 10;
    bool alreadyAttacked = false;
    public float m_AttackSpeed = 2f;
    public GameObject weapon;
    bool EnemyHit = false;

    //Shooting
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public bool isShooting;

    //Sound Effect
    public AudioClip walking;
    public AudioClip running;
    public AudioClip fight;
    public AudioClip e_shooting;
    public AudioClip death;
    public AudioClip getHit;

    public AudioSource audio;

    public bool stopMoving;

    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Start()
    {
        playerPoint = GameObject.FindWithTag("PlayerPoint").transform;
    }  

    private void Update()
    {
        //Setting Sight range and Attack range, create a sphere(position, radius and layerMask)
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInRangeAttackRange = Physics.CheckSphere(transform.position, rangeAttackRange, whatIsPlayer);

        //Setting AI action in different situration
        if (!playerInSightRange && !playerInAttackRange && !dead && !playerInRangeAttackRange && !stopMoving) Defence();
        if (playerInSightRange && !playerInAttackRange && !dead && !stopMoving) ChasePlayer();
        if (playerInRangeAttackRange && !playerInSightRange && !playerInAttackRange && !dead) ShootPlayer();
        if (playerInSightRange && playerInAttackRange && !dead) AttackPlayer();
    }

    //Mutli-point defence or only set 1 defence point 
    private void Defence()
    {
        if (defPointIndex <= defPoint.Length - 1)
        {        
            agent.speed = walkSpeed;

            agent.SetDestination(defPoint[defPointIndex].transform.position);
            //transform.LookAt(wayPoint[wayPointIndex]);

            defPoint[defPointIndex].position = new Vector3(defPoint[defPointIndex].position.x, transform.position.y, defPoint[defPointIndex].position.z);
            transform.LookAt(defPoint[defPointIndex]);
            animator.SetBool("Moving", true);
            animator.SetBool("Attack", false);
            animator.SetBool("Running", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Shoot", false);


            //if object location = current location, Index +1
            Vector3 distanceToWalkPoint = transform.position - defPoint[defPointIndex].position;
            if (distanceToWalkPoint.magnitude < 1f)
            {
                defPointIndex += 1;
                animator.SetBool("Idle", true);
                transform.rotation = Quaternion.Euler(0, defDirection, 0);
                audio.Stop();
            }
        }

        //when object reachs the last point, reset to 0
        if (defPointIndex == defPointNumber) // <-- this number = number of way Point
        {
            defPointIndex = 0;
        }
    }

    //moving forward and looking at the player
    private void ChasePlayer()
    {
        player = GameHandler.instance.GetPlayer();
        agent.speed = chaseSpeed;
        
        animator.SetBool("Running", true);
        animator.SetBool("Moving", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Shoot", false);

        if (player != null)
        {
            agent.SetDestination(player.position);

            playerPoint.position = new Vector3(playerPoint.position.x, transform.position.y, playerPoint.position.z);
            transform.LookAt(playerPoint);

        }
    }

    //Attack and look at the player, also having between time again. Need to add attack script for the attack function.
    private void AttackPlayer()
    {
        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            player = GameHandler.instance.GetPlayer();

            agent.SetDestination(transform.position);
            if (player != null)
            {
                stopMoving = true;

                animator.SetBool("Attack", true);
                animator.SetBool("Running", false);
                animator.SetBool("Moving", false);
                animator.SetBool("Idle", false);
                animator.SetBool("Shoot", false);

                playerPoint.position = new Vector3(playerPoint.position.x, transform.position.y, playerPoint.position.z);
                transform.LookAt(playerPoint);
            }
            StartCoroutine(ResetAttack());
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(m_AttackSpeed);
        alreadyAttacked = false;
    }

    public void WeaponHited()
    {
        audio.clip = fight; audio.loop = false; audio.Play();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().DecreaseHealth(m_Damage);
    }

    public void ShootPlayer()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Running", false);
        animator.SetBool("Moving", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Shoot", true);

        stopMoving = true;

        playerPoint.position = new Vector3(playerPoint.position.x, transform.position.y, playerPoint.position.z);
        transform.LookAt(playerPoint);

        agent.speed = 0;
    }

    public void Shooting()
    {
        GameObject.Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        //audio.clip = e_shooting; audio.loop = false; audio.Play();
    }

    public void Hit()
    {
        if (!EnemyHit && !dead)
        {
            StartCoroutine(HitRoutine());
        }
    }

    public void Hitted()
    {
        stopMoving = false;
    }

    IEnumerator HitRoutine()
    {
        EnemyHit = true;
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(0.5f);
        EnemyHit = false;
    }

    public void Death()
    {
        dead = true;
        agent.velocity = Vector3.zero;
        agent.acceleration = 0;
        //agent.transform.position = Vector3.zero;
        animator.SetTrigger("Death");
    }

    public bool IsDead()
    {
        return dead;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, rangeAttackRange);
    }

    //Audio play, animation event
    public void walksSound()
    {
        audio.clip = walking; audio.loop = true; audio.Play();
    }

    public void runSound()
    {
        audio.clip = running; audio.loop = true; audio.Play();
    }
}
