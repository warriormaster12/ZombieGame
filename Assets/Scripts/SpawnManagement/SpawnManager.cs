using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_spawn_points;
    [SerializeField] private float m_spawn_interval;

    [SerializeField] private int m_max_zombie_count = 10;

    private float timer = 0f;
    private int zombie_count = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        zombie_count = GameObject.FindGameObjectsWithTag("Zombie").Length;
        if (m_spawn_points.Count == 0 || zombie_count == m_max_zombie_count) {
            timer = 0f;
            return;
        }

        timer += Time.fixedDeltaTime;
        if (timer >= m_spawn_interval) {
            SpawnPoint spawn_point = m_spawn_points[Random.Range(0, m_spawn_points.Count - 1)].GetComponent<SpawnPoint>();
            if (!spawn_point) {
                Debug.LogError("No SpawnPoint component found");
                return;
            }
            spawn_point.SpawnZombie();
            Debug.Log(zombie_count);
            timer = 0f;
        }
    }
}
