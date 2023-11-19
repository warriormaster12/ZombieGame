using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_spawn_points;
    [SerializeField] private Vector2 m_spawn_interval = new Vector2(1.0f, 2.5f);

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
        if (timer >= Random.Range(m_spawn_interval.x, m_spawn_interval.y)) {
            int idx = Random.Range(0, m_spawn_points.Count);
            SpawnPoint spawn_point = m_spawn_points[idx].GetComponent<SpawnPoint>();
            if (!spawn_point) {
                Debug.LogError("No SpawnPoint component found");
                return;
            }
            spawn_point.SpawnZombie();
            timer = 0f;
        }
    }
}
