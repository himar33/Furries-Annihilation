using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected enum EnemyState
    {
        MOVE,
        ATTACK,
        DIE,
        HIT,
    }

    [SerializeField]
    protected Transform playerPos;
    [SerializeField]
    protected PlayerManager player;
    [SerializeField]
    protected float dmgHit;

    [SerializeField]
    protected EnemyState state;

    [SerializeField]
    protected ParticleSystem deathParticle;

    protected NavMeshAgent agent;
    protected Animator anim;
    protected bool canHit = false;

    protected virtual void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        state = EnemyState.MOVE;
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerManager>();
    }

    protected virtual void Update()
    {
        switch (state)
        {
            case EnemyState.MOVE:
                agent.SetDestination(playerPos.position);
                break;
            case EnemyState.ATTACK:
                break;
            case EnemyState.DIE:
                anim.SetTrigger("Spin");
                break;
            case EnemyState.HIT:
                break;
            default:
                break;
        }
    }

    protected void Explosion()
    {
        ParticleSystem particles = Instantiate(deathParticle, transform.position, transform.rotation, GameObject.Find("pendingToDelete").transform);

        Destroy(particles.gameObject, 2.0f);

        Destroy(gameObject);
    }

    protected void Hit()
    {
        if (canHit) player.Hitted(dmgHit);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerRange")
        {
            canHit = true;

            state = EnemyState.ATTACK;
            agent.isStopped = true;
            anim.SetTrigger("Clicked");
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerRange")
        {
            canHit = false;

            state = EnemyState.MOVE;
            agent.isStopped = false;
            anim.SetTrigger("Walk");
        }
    }
}
