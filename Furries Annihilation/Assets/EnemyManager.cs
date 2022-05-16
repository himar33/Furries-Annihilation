using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    enum EnemyState
    {
        MOVE,
        ATTACK,
        DIE,
        HIT,
    }

    [SerializeField]
    private Transform playerPos;

    private EnemyState state;

    private NavMeshAgent agent;

    private Collider range;

    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(playerPos.position);

        range = GetComponent<Collider>();

        state = EnemyState.MOVE;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.MOVE:
                agent.SetDestination(playerPos.position);
                break;
            case EnemyState.ATTACK:
                break;
            case EnemyState.DIE:
                break;
            case EnemyState.HIT:
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerRange")
        {
            state = EnemyState.ATTACK;
            agent.isStopped = true;
            anim.SetTrigger("Clicked");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerRange")
        {
            state = EnemyState.MOVE;
            agent.isStopped = false;
            anim.SetTrigger("Walk");
        }
    }
}
