using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsPlayer, whatIsGround;

    public Vector3 walkPoint;

    bool walkPointSet;
    bool alreadyAttacked;

    public float walkPointRange;
    public float timeBetweenAttacks;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {
        player = GameObject.Find("Swat").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void AttackPlayer() 
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);

        }
    }
    void ResetAttack() 
    {
        alreadyAttacked = false;
    }
    public void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) { agent.SetDestination(walkPoint); }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
        
        
    }
    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    void SearchWalkPoint() 
    {
        float randomZ = Random.Range(-walkPointRange,walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,transform.position.y + transform.position.z + randomZ );

        if (Physics.Raycast(walkPoint,- transform.up,2f,whatIsGround))
        {
            walkPointSet = true;
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer) ;
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInAttackRange & !playerInSightRange) Patrolling();
        if (playerInAttackRange & !playerInSightRange) ChasePlayer();
        if (playerInAttackRange & playerInSightRange) AttackPlayer();
    }
}
