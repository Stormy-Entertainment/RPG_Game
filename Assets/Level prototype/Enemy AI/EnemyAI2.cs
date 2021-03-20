using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI2 : MonoBehaviour
{  
    public Animator animator;

    public NavMeshAgent agent;
    private Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patoring
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    //Attacking
    public float timeBetweenAttacks;
    public float m_Damage = 20;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool dead = false;

    //Find player object by name
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        //Setting Sight range and Attack range, create a sphere(position, radius and layerMask)
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //Setting AI action in different situration
        if (!playerInSightRange && !playerInAttackRange && !dead) Patroling();
        if (playerInSightRange && !playerInAttackRange && !dead) ChasePlayer();
        if (playerInSightRange && playerInAttackRange && !dead) AttackPlayer();

    }



    //AI wandering on the level, also looking at the walkpoint
    private void Patroling()
    {
        animator.SetBool("Moving", true);
        animator.SetBool("Attack", false);

        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 3f) walkPointSet = false;

        //transform.rotation = Quaternion.LookRotation(walkPoint);
        transform.LookAt(walkPoint);
    }

    private void SearchWalkPoint()
    {
        //Random pick a walkPoint(X & Z) from the walkpoint range, blue sphere line showing the range 
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //using raycast for ground check
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
                walkPointSet = true;
        }
    }


    //moving forward and looking at the player
    private void ChasePlayer()
    {
        player = GameHandler.instance.GetPlayer();
        animator.SetBool("Moving", true);
        animator.SetBool("Attack", false);

        if (player != null)
        {
            agent.SetDestination(player.position);
            transform.LookAt(player);
        }
    }

    //Attack and look at the player, also having between time again. Need to add attack script for the attack function.
    private void AttackPlayer()
    {
        player = GameHandler.instance.GetPlayer();
        animator.SetBool("Attack", true);

        agent.SetDestination(transform.position);
        if (player != null)
        {
            transform.LookAt(player);
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

    //Showing range area in unity with different color
    void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(walkPoint, new Vector3(1, 1, 1));
    }

}
