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

    [Header("Model Data")]
    [SerializeField]
    protected Transform playerPos;
    [SerializeField]
    protected PlayerManager player;

    [Header("Stats")]
    [SerializeField]
    protected float velocity;
    [SerializeField]
    protected float dmgHit;
    protected int life;

    [SerializeField]
    protected EnemyState state;

    [SerializeField]
    protected ParticleSystem deathParticle;

    protected NavMeshAgent agent;
    protected Animator anim;
    protected bool canHit = false;
    protected AudioSource hitSFX;

    protected virtual void Start()
    {
        state = EnemyState.MOVE;
        life = 4;

        playerPos = GameObject.Find("Player").transform;
        player = FindObjectOfType<PlayerManager>();

        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();
        anim.SetTrigger("Walk");
        hitSFX = GameObject.Find("hitSFX").GetComponent<AudioSource>(); 
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
        FindObjectOfType<LevelManager>().currEnemiesAlive--;
        FindObjectOfType<LevelManager>().enemiesUI.text = "ENEMIES LEFT: " + FindObjectOfType<LevelManager>().currEnemiesAlive;
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

        if (other.tag == "Bullet")
        {
            hitSFX.PlayOneShot(hitSFX.clip);
            life--;
            if (life < 0)
            {
                state = EnemyState.DIE;
            }
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
