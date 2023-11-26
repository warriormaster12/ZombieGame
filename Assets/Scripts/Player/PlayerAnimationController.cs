using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject m_player_character;

    private Animator animator = null;
    private PlayerController player_controller = null;

    private int direction_x_hash;
    private int direction_y_hash;
    void Awake()
    {
        animator = GetComponent<Animator>();
        if (m_player_character) {
            player_controller = m_player_character.GetComponent<PlayerController>();
        }
        direction_x_hash = Animator.StringToHash("direction_x");
        direction_y_hash = Animator.StringToHash("direction_y");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player_controller.GetDirection();
        Vector3 out_direction = transform.InverseTransformDirection(direction);
        animator.SetFloat(direction_x_hash, out_direction.x);
        animator.SetFloat(direction_y_hash, out_direction.z);
    }
}
