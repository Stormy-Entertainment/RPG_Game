using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_EnemyDef : MonoBehaviour
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
    public float chaseSpeed;
    public float defDirection;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool dead = false;

    //Attacking
    public float timeBetweenAttacks;
    public float m_Damage = 20;
    bool alreadyAttacked;

    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerPoint = GameObject.Find("PlayerPoint").transform;
    }

    //Mutli-point defence or only set 1 defence point 
    private void Defence()
    {
        if (defPointIndex <= defPoint.Length - 1)
        {
            agent.SetDestination(defPoint[defPointIndex].transform.position);
            //transform.LookAt(wayPoint[wayPointIndex]);

            defPoint[defPointIndex].position = new Vector3(defPoint[defPointIndex].position.x, transform.position.y, defPoint[defPointIndex].position.z);
            transform.LookAt(defPoint[defPointIndex]);
            animator.SetBool("Moving", true);
            animator.SetBool("Attack", false);


            //if object location = current location, Index +1
            Vector3 distanceToWalkPoint = transform.position - defPoint[defPointIndex].position;
            if (distanceToWalkPoint.magnitude < 1f)
            {
                defPointIndex += 1;

                animator.SetBool("Moving", false);
                transform.rotation = Quaternion.Euler(0, defDirection, 0);
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
        //player = GameHandler.instance.GetPlayer();
        agent.speed = chaseSpeed;
        player = GameObject.Find("Player").transform;
        animator.SetBool("Moving", true);
        animator.SetBool("Attack", false);

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
            //player = GameHandler.instance.GetPlayer();
            player = GameObject.Find("Player").transform;

            animator.SetBool("Attack", true);

            agent.SetDestination(transform.position);
            if (player != null)
            {
                playerPoint.position = new Vector3(playerPoint.position.x, transform.position.y, playerPoint.position.z);
                transform.LookAt(playerPoint);
            }


            if (!alreadyAttacked)
            {
                alreadyAttacked = true;
                player.GetComponent<PlayerStats>().DecreaseHealth(m_Damage);
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

    private void ResetAttack()
    {
        alreadyAttacked = false;
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

    void Update()
    {
        //Setting Sight range and Attack range, create a sphere(position, radius and layerMask)
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //Setting AI action in different situration
        if (!playerInSightRange && !playerInAttackRange && !dead) Defence();
        if (playerInSightRange && !playerInAttackRange && !dead) ChasePlayer();
        if (playerInSightRange && playerInAttackRange && !dead) AttackPlayer();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
