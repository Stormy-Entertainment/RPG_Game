using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_wanderer : MonoBehaviour
{
    public Animator animator;
    public UnityEngine.AI.NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;

    //Wanderer
    public Transform walkArea;
    public float walkPointRange;
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkSpeed;

    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    //Searching walk point inside the AI and NavMesh area. I set the next point have (min. +4). 
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(walkArea.position.x - randomX, transform.position.y, walkArea.position.z - randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            if (RandomPoint())
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
        //animator.SetBool("Attack", false);

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

    void Update()
    {
        WalkToPoint();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(walkArea.position, walkPointRange);
    }
}
