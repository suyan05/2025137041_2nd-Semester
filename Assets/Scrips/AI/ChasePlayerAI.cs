using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerAI : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 50f;
    public float attackRange = 2f;

    private NavMeshAgent agent;
    private float distanceToPlayer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
                
        if (distanceToPlayer <= chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasing();
        }

        if(distanceToPlayer <= attackRange)
        {
            Attack();
        }
    }

    private void StopChasing()
    {
        agent.isStopped = true;
    }

    private void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        agent.isStopped = true;
        transform.LookAt(player);
        Debug.Log("Attacking player!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
