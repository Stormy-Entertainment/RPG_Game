﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;

    public UnityEngine.AI.NavMeshAgent agent;
    private Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Transform playerPoint;

    //Defence point
    public Transform defPoint;

    //Attacking
    public float m_Damage = 20;
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool dead = false;

    //Find player object by name
    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Update()
    {
        //Setting Sight range and Attack range, create a sphere(position, radius and layerMask)
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && !dead) Defence();
        if (playerInSightRange && !playerInAttackRange && !dead) ChasePlayer();
        if (playerInSightRange && playerInAttackRange && !dead) AttackPlayer();

        
    }


    //Back to the defence, if no player find 
    private void Defence()
    {
        //transform.position = Vector3.MoveTowards(transform.position, defPoint.position, 0.04f);
        Vector3 distanceToDefPoint = transform.position - defPoint.position;

        agent.destination = defPoint.position;

        defPoint.position = new Vector3(defPoint.position.x, transform.position.y, defPoint.position.z);
        transform.LookAt(defPoint);
        

        if (distanceToDefPoint.magnitude < 1f)
        {
            animator.SetBool("Moving", false);
            //agent.SetDestination(transform.position);
            
            transform.rotation = Quaternion.Euler(0, defPoint.rotation.y, 0);
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

            playerPoint.position = new Vector3(playerPoint.position.x, transform.position.y, playerPoint.position.z);
            transform.LookAt(playerPoint);
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
            playerPoint.position = new Vector3(playerPoint.position.x, transform.position.y, playerPoint.position.z);
            transform.LookAt(playerPoint);
            //transform.LookAt(player);
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
