using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private HealthComponent health;
    private Animator animator;
    private NavMeshAgent nav_mesh;
    void Awake()
    {
        health = GetComponent<HealthComponent>();
        if (health) {
            health.OnHealthDepleeted.AddListener(OnDeath);
        }  
        animator = transform.GetChild(0).GetComponent<Animator>();
        nav_mesh = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (animator) {
            if (nav_mesh) {
                animator.SetFloat("speed", nav_mesh.velocity.normalized.magnitude);
            }
        }
    }

    void OnDeath() {
        Destroy(gameObject);
    }
}
