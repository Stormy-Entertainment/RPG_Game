using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;

    public UnityEngine.AI.NavMeshAgent agent;
    private Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

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
    private StageMenuUI stageMenu;

    //Find player object by name
    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Start()
    {
        stageMenu = FindObjectOfType<StageMenuUI>();
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
        agent.destination = defPoint.position;
        transform.LookAt(defPoint);

        //transform.position = Vector3.MoveTowards(transform.position, defPoint.position, 0.04f);
        Vector3 distanceToDefPoint = transform.position - defPoint.position;
        if (distanceToDefPoint.magnitude < 0.1f)
        {
            animator.SetBool("Moving", false);
            agent.SetDestination(transform.position);
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
        if (!dead)
        {
            dead = true;
            agent.velocity = Vector3.zero;
            agent.acceleration = 0;
            animator.SetTrigger("Death");
            BossParticle bossParticle = GetComponent<BossParticle>();
            if (bossParticle != null)
            {
                bossParticle.ActivateParticles();
            }
            SFXManager.GetInstance().PlaySound("StageCompleted");
            StartCoroutine(StageCompleted());
        }
    }

    private IEnumerator StageCompleted()
    {
        yield return new WaitForSeconds(6f);
        stageMenu.LevelCompleted();
        Destroy(gameObject);
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
