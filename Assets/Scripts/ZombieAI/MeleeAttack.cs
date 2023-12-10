using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float m_damage = 20f;
    [SerializeField] private string m_target_tag = "Player";
    [SerializeField] private float m_ray_length = 15f;

    [SerializeField] private LayerMask m_ray_layer_mask;

    private bool can_attack = false;

    private ZombieAnimationEvents animation_events;

    void Awake()
    {
        animation_events = transform.GetChild(0).GetComponent<ZombieAnimationEvents>();
    }
    void Start()
    {
        animation_events.OnStartAttack.AddListener(OnStartAttack);
        animation_events.OnEndAttack.AddListener(OnEndAttack);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (can_attack)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), transform.forward, out hit, m_ray_length, m_ray_layer_mask))
            {
                if (hit.collider.tag == m_target_tag)
                {
                    HealthComponent health = hit.collider.gameObject.GetComponent<HealthComponent>();
                    if (!health)
                    {
                        return;
                    }
                    health.SetHealth(health.GetHealth() - m_damage);
                }
            }
        }
    }

    void OnStartAttack()
    {
        can_attack = true;
    }

    void OnEndAttack()
    {
        can_attack = false;
    }
}
