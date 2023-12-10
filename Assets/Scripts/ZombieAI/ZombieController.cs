using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private HealthComponent health;
    private Animator animator;
    private NavMeshAgent nav_mesh;
    private MoveTo move_to;
    private MeleeAttack meleeAttack;

    private ZombieAnimationEvents animation_events;

    private bool is_player_dead = false;
    void Awake()
    {
        health = GetComponent<HealthComponent>();
        if (health)
        {
            health.OnHealthDepleeted.AddListener(OnDeath);
        }
        animator = transform.GetChild(0).GetComponent<Animator>();
        animation_events = animator.gameObject.GetComponent<ZombieAnimationEvents>();
        nav_mesh = GetComponent<NavMeshAgent>();
        move_to = GetComponent<MoveTo>();
        meleeAttack = GetComponent<MeleeAttack>();
    }

    void Start()
    {
        if (move_to)
        {
            move_to.GetTarget().GetComponent<HealthComponent>().OnHealthDepleeted.AddListener(OnPlayerDeath);
        }
        if (animation_events)
        {
            animation_events.OnDeathFinished.AddListener(() => { StartCoroutine(OnDeathFinished()); });
        }
    }

    void Update()
    {
        if (animator)
        {
            if (nav_mesh)
            {
                if (!move_to)
                {
                    animator.SetFloat("speed", nav_mesh.velocity.normalized.magnitude);
                }
                else
                {
                    animator.SetFloat("speed", nav_mesh.velocity.magnitude / move_to.GetSpeed());
                }
                animator.SetBool("attacking", move_to.TargetReached() && !is_player_dead);

            }
        }
    }

    void OnDeath()
    {
        if (!animator)
        {
            Destroy(gameObject);
        }
        nav_mesh.enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        animator.SetBool("dying", true);
    }

    IEnumerator OnDeathFinished()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(gameObject);
    }

    void OnPlayerDeath()
    {
        animator.SetBool("dancing", true);
        nav_mesh.enabled = false;
        is_player_dead = true;
    }
}
