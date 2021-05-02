using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_Enemy2 : MonoBehaviour
{
    private Transform playerPoint;
    private Transform player;

    public Animator animator;
    public UnityEngine.AI.NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;

    //Wanderer
    public Transform walkArea;
    public float walkPointRange;
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkSpeed;
    public float chaseSpeed;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool dead = false;

    //Attacking
    public float timeBetweenAttacks;
    public int m_Damage = 20;
    bool alreadyAttacked;

    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        playerPoint = GameObject.Find("PlayerPoint").transform;
    }

    //Searching walk point inside the AI and NavMesh area. 
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(walkArea.position.x - randomX, transform.position.y, walkArea.position.z - randomZ);
        
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            if(RandomPoint())
            {
                walkPointSet = true;
            }            
        }

        //reture true when the position inside NavMesh
        bool RandomPoint()
        {
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(walkPoint, out hit, walkPointRange, 1))
            {
                walkPoint = hit.position;
            }
            return true;
        }      
    }

    //Walk to the walk point, call search walk point when arrive
    private void WalkToPoint()
    {
        animator.SetBool("Moving", true);
        animator.SetBool("Attack", false);
        animator.SetBool("Running", false);

        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.speed = walkSpeed;
            agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            if (distanceToWalkPoint.magnitude < 2f) walkPointSet = false;
        }
    }

    //moving forward and looking at the player
    private void ChasePlayer()
    {
        //player = GameHandler.instance.GetPlayer();
        agent.speed = chaseSpeed;
        player = GameObject.Find("Player").transform;
        animator.SetBool("Running", true);
        animator.SetBool("Moving", false);
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
        animator.SetBool("Running", false);
        animator.SetBool("Moving", false);

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
        if (!playerInSightRange && !playerInAttackRange && !dead) WalkToPoint();
        if (playerInSightRange && !playerInAttackRange && !dead) ChasePlayer();
        if (playerInSightRange && playerInAttackRange && !dead) AttackPlayer(); 
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(walkArea.position, walkPointRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
