using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterScript : MonoBehaviour
{
    GameObject player;
    public float speed = 2.0f;
    public float detectionRange = 5.0f;
    public float chaseRange = 10.0f;

    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    NavMeshAgent agent;

    public float stunTimer = 3.0f;

    bool playerDetected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player");

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);


        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == player)
            {
                playerDetected = true;
                break;
            }
        }

        if (playerDetected)
        {
            ChasePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Patrol Point"))
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }

        if (other.gameObject.CompareTag("ball1"))
        {
            StartCoroutine(Stun());
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }

    IEnumerator Stun()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(stunTimer);
        agent.isStopped = false;
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, chaseRange);

        if (!hitColliders.Contains(player.GetComponent<Collider>()))
        {
            playerDetected = false;
            if (patrolPoints.Length > 0)
            {
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            }
        }
    }
}
