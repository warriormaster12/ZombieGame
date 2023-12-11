using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    [SerializeField] private float m_lifetime = 5.0f;
    [SerializeField] private float m_speed = 20f;
    private float time = 0.0f;

    private float damage = 0.0f;

    private Vector3 direction = Vector3.zero;

    public void SetupProjectile(Vector3 dir, Vector3 spawn_position, float damage)
    {
        direction = dir;
        transform.position = spawn_position;
        this.damage = damage;
    }
    void FixedUpdate()
    {
        transform.position += direction * m_speed * Time.fixedDeltaTime;

        time += Time.fixedDeltaTime;
        if (time >= m_lifetime)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie" && other.gameObject.GetComponent<HealthComponent>())
        {
            HealthComponent component = other.gameObject.GetComponent<HealthComponent>();
            component.SetHealth(component.GetHealth() - damage);
            Destroy(gameObject);
        }
    }
}
