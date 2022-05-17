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
    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private float dmgHit;

    [SerializeField]
    private EnemyState state;

    [SerializeField]
    private ParticleSystem deathParticle;

    private NavMeshAgent agent;
    private Animator anim;
    private bool canHit = false;

    void Start()
    {
        playerPos = GameObject.Find("Player").transform;

        agent = GetComponent<NavMeshAgent>();

        state = EnemyState.MOVE;

        anim = GetComponent<Animator>();

        player = FindObjectOfType<PlayerManager>();
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
                anim.SetTrigger("Spin");
                break;
            case EnemyState.HIT:
                break;
            default:
                break;
        }
    }

    private void Explosion()
    {
        ParticleSystem particles = Instantiate(deathParticle, transform.position, transform.rotation, GameObject.Find("pendingToDelete").transform);

        Destroy(particles.gameObject, 2.0f);

        Destroy(gameObject);
    }

    private void Hit()
    {
        if (canHit) player.Hitted(dmgHit);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerRange")
        {
            canHit = true;

            state = EnemyState.ATTACK;
            agent.isStopped = true;
            anim.SetTrigger("Clicked");
        }
    }

    private void OnTriggerExit(Collider other)
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
