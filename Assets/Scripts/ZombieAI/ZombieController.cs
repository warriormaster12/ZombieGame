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
    void Awake()
    {
        health = GetComponent<HealthComponent>();
        if (health)
        {
            health.OnHealthDepleeted.AddListener(OnDeath);
        }
        animator = transform.GetChild(0).GetComponent<Animator>();
        nav_mesh = GetComponent<NavMeshAgent>();
        move_to = GetComponent<MoveTo>();
    }

    void Start()
    {
        if (move_to)
        {
            move_to.GetTarget().GetComponent<HealthComponent>().OnHealthDepleeted.AddListener(OnPlayerDeath);
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

    void OnPlayerDeath()
    {
        animator.SetBool("dancing", true);
        nav_mesh.enabled = false;
    }
}
