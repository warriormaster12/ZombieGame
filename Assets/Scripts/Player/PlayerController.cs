using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed = 5.0f;
    [SerializeField] private float m_gravity = 9.81f;
    [SerializeField] private float m_mass = 3f;
    [SerializeField] private float m_acceleration = 10f;

    [SerializeField] private Transform m_model_container;
    
    
    bool grounded;
    Vector3 velocity;
    Vector3 direction;

    CharacterController controller;
    void Awake() {
        controller = GetComponent<CharacterController>();
    }

    public Vector3 GetDirection() {
        return direction;
    }

    private void FixedUpdate() {
        if (m_model_container) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane p = new Plane( Vector3.up, transform.position );
            if( p.Raycast( mouseRay, out float hitDist) ){
                Vector3 hitPoint = mouseRay.GetPoint( hitDist );
                m_model_container.LookAt(hitPoint);
                m_model_container.rotation = Quaternion.Euler(new Vector3(0, m_model_container.rotation.eulerAngles.y, 0));
            }

        }
        grounded = controller.isGrounded;
        if (grounded && velocity.y < 0)
        {
            velocity.y = 0f;
        } else {
            velocity.y = -m_gravity * m_mass * Time.fixedDeltaTime;
        }

        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        velocity = Vector3.Lerp(velocity, direction * m_speed * Time.fixedDeltaTime, m_acceleration * Time.fixedDeltaTime);
        controller.Move(velocity);
    }
    
}
