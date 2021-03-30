using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class k_enemy : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;

    private Transform player;
    private Transform playerPoint;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patoring
    public Transform walkArea;
    private Vector3 walkPoint;
    public bool walkPointSet;
    //public float walkPointRange;
    public float radius;
 

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
        agent = GetComponent<NavMeshAgent>();
        playerPoint = GameObject.Find("PlayerPoint").transform;
        

    }


    void Update()
    {
        //Setting Sight range and Attack range, create a sphere(position, radius and layerMask)
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //Setting AI action in different situration
        if (!playerInSightRange && !playerInAttackRange && !dead) Patroling();
        if (playerInSightRange && !playerInAttackRange && !dead) ChasePlayer();
        if (playerInSightRange && playerInAttackRange && !dead) AttackPlayer();
    }
    
    
    private void SearchWalkPoint()
    {
        //walkPoint = new Vector3 (finalPosition);
        if (walkPointSet = false)
        {
            Debug.Log("1");
            
            walkPointSet = true;
        }


        /*
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(walkArea.position.x - randomX, transform.position.y, walkArea.position.z - randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
        */
    }
    

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        UnityEngine.AI.NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }  
            return finalPosition;
    }

    private void Patroling()
    {
        agent.SetDestination(walkPoint);

        //transform.LookAt(RandomNavmeshLocation(radius));
        //animator.SetBool("Moving", true);
        //animator.SetBool("Attack", false);
        /*
        if (!walkPointSet) SearchWalkPoint();
        agent.SetDestination(walkPoint);
        transform.LookAt(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
        */
    }


    //moving forward and looking at the player
    private void ChasePlayer()
    {
        //player = GameHandler.instance.GetPlayer();
        //animator.SetBool("Moving", true);
        //animator.SetBool("Attack", false);

        player = GameObject.Find("Player").transform;

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

        //animator.SetBool("Attack", true);

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
        //animator.SetTrigger("Death");
    }

    public bool IsDead()
    {
        return dead;
    }


    void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawCube(walkPoint, new Vector3(1, 1, 1));
    }

}
