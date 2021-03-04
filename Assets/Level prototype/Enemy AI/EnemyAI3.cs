using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI3 : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //patrol point
    public Transform[] wayPoint;
    public int wayPointNumber;
    private int wayPointIndex = 0;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    void Start()
    {
        transform.position = wayPoint[wayPointIndex].transform.position;
    }

    //Find player object by name
    private void Awake()
    {
        player = GameObject.Find("Hero").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Update()
    {
        //Setting Sight range and Attack range, create a sphere(position, radius and layerMask)
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Defence();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

    }

    public void Defence()
    {
        if (wayPointIndex <= wayPoint.Length - 1)
        {
            //object moves toward to the index position, 
            transform.position = Vector3.MoveTowards(transform.position, wayPoint[wayPointIndex].transform.position, 0.02f);
            transform.LookAt(wayPoint[wayPointIndex]);

            //if object location = current location, Index +1
            if (transform.position == wayPoint[wayPointIndex].transform.position)
            {
                wayPointIndex += 1;
            }
        }

        //when object reachs the last point, reset to 0
        if (wayPointIndex == wayPointNumber) // <-- this number = number of way Point
        {
            wayPointIndex = 0;
        }
    }


    //moving forward and looking at the player
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }

    //Attack and look at the player, also having between time again. Need to add attack script for the attack function.
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);


        if (!alreadyAttacked)
        {
            print("hit");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


    //Showing range area in unity with different color
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }

}
