using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_zombie_types;

    private Collider spawn_collider;

    void Awake() {
        spawn_collider = GetComponent<Collider>();
        if (!spawn_collider) {
            Debug.LogWarning("No collider component found " + name);
        }
    }

    public void SpawnZombie() {
        GameObject zombie = m_zombie_types[Random.Range(0, m_zombie_types.Count - 1)];
        GameObject zombie_inst = Instantiate(zombie);
        zombie_inst.transform.position =  new Vector3(
            Random.Range(spawn_collider.bounds.min.x, spawn_collider.bounds.max.x),
            transform.position.y,
            Random.Range(spawn_collider.bounds.min.z, spawn_collider.bounds.max.z)
        );
    }
}
