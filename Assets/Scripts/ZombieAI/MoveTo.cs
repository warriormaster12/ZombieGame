using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    [SerializeField] private string m_target_tag = "Player";
    [SerializeField] private bool m_running = true;
    [SerializeField] private float m_speed = 5f;
    [SerializeField] private float m_acceptable_distance = 2f;

    private NavMeshAgent nav_mesh = null;

    private MeleeAttack melee_attack = null;
    private Vector3 last_position = Vector3.zero;

    private GameObject target = null;

    public float GetSpeed()
    {
        return m_speed;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    void Awake()
    {
        nav_mesh = GetComponent<NavMeshAgent>();
        if (!nav_mesh)
        {
            Debug.LogWarning("Can't run MoveTo script due to missing NavMeshAgent component");
        }
        melee_attack = GetComponent<MeleeAttack>();
        if (!melee_attack)
        {
            Debug.LogWarning("melee_attack == null, zombie can't hit you. Add script");
        }
        else
        {
            melee_attack.enabled = false;
        }
        target = GameObject.FindGameObjectWithTag(m_target_tag);
        if (!target)
        {
            Debug.LogWarning("Can't run MoveTo script due to target not being found by tag: " + m_target_tag);
        }
        else
        {
            last_position = target.transform.position;
        }
    }
    void FixedUpdate()
    {
        if (!nav_mesh || !target)
        {
            target = GameObject.FindGameObjectWithTag(m_target_tag);
            return;
        }
        if (!nav_mesh.enabled)
        {
            return;
        }
        if (last_position != target.transform.position || nav_mesh.destination != target.transform.position)
        {
            nav_mesh.destination = target.transform.position;
            nav_mesh.speed = m_running ? m_speed : m_speed / 2;
            nav_mesh.stoppingDistance = m_acceptable_distance;
            last_position = target.transform.position;
        }
        if (melee_attack)
        {
            melee_attack.enabled = nav_mesh.remainingDistance <= m_acceptable_distance;
        }
    }
}
