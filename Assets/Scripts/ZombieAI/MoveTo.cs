using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    [SerializeField] private string m_target_tag = "Player";
    [SerializeField] private float m_speed = 5f;
    [SerializeField] private float m_acceptable_distance = 2f;

    private NavMeshAgent nav_mesh = null;
    private Vector3 last_position = Vector3.zero; 

    private GameObject target = null;
    void Awake() {
        nav_mesh = GetComponent<NavMeshAgent>();
        if (!nav_mesh) {
            Debug.LogWarning("Can't run MoveTo script due to missing NavMeshAgent component");
        }
        target = GameObject.FindGameObjectWithTag(m_target_tag);
        if (!target) {
            Debug.LogWarning("Can't run MoveTo script due to target not being found by tag: " + m_target_tag);
        } else {
            last_position = target.transform.position;
        }
    }
    void FixedUpdate() {
        if (!nav_mesh || !target) {
            target = GameObject.FindGameObjectWithTag(m_target_tag);
            return;
        }
        if (last_position != target.transform.position) {
            nav_mesh.destination = target.transform.position;
            nav_mesh.speed = m_speed;
            nav_mesh.stoppingDistance = m_acceptable_distance;
            last_position = target.transform.position;
        }
    }
}
