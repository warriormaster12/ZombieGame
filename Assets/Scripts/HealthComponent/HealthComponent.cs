using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float m_health = 100f;

    private float current_health = 0f;

    void Awake() {
        current_health = m_health;
    }

    public float GetHealth() {
        return current_health;
    } 
    public void SetHealth(float value) {
        current_health = value;
        current_health = Mathf.Clamp(current_health, 0f, m_health);
        if (current_health <= 0.0f) {
            Destroy(gameObject);
        }
    }
}
