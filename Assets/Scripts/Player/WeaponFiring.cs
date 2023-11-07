using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFiring : MonoBehaviour
{
    [SerializeField] private Transform m_gun_attachment;

    private WeaponSystem current_weapon;
    private void Awake() {
        if (m_gun_attachment) {
            foreach(Transform child in m_gun_attachment) {
                if (child.gameObject.activeSelf && child.GetComponent<WeaponSystem>()) {
                   current_weapon = child.GetComponent<WeaponSystem>();
                   break;  
                }
            }
        }
    }

    private void Update() {
        if (Input.GetButton("Fire1")) {
            if (current_weapon) {
                StartCoroutine(current_weapon.FireWeapon());
            }
        }
    }
}
