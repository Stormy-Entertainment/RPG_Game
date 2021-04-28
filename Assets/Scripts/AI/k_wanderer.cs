using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_wanderer : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public UnityEngine.AI.NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;

    //Wanderer
    public Transform walkArea;
    public float walkPointRange;
    private Vector3 walkPoint;
    bool walkPointSet;
    bool howling = false;
    public float walkSpeed;
    int walkPointCounter = 0;

    public bool stopWalking = false;

    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (!howling)
        {
            WalkToPoint();
        }

        if(stopWalking == true)
        {
            agent.speed = 0f;
        }
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
            //animator.SetBool("Moving", true);
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
                if (distanceToWalkPoint.magnitude < 2f) 
                {
                    walkPointSet = false;
                    walkPointCounter++;
                    if(walkPointCounter >= 10)
                    {
                         Howl();
                         walkPointCounter = 0;             
                    }
                 }
            }
    }

    private void Howl()
    {
        if (gameObject.tag == "Wolf")
        {
            stopWalking = true;
            howling = true;
            animator.SetTrigger("howl");
            if (audioSource != null)
            {
                audioSource.Play();
            }
            StartCoroutine(ResetHowl());
        }
    }

    private IEnumerator ResetHowl()
    {
        yield return new WaitForSeconds(3f);
        howling = false;
    }

    public void AfterHowl()
    {
        stopWalking = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(walkArea.position, walkPointRange);
    }
}
