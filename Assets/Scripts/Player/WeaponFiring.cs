using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFiring : MonoBehaviour
{
    [SerializeField] private Transform m_gun_attachment;

    private WeaponSystem current_weapon;
    private int current_weapon_index = 0;
    private int all_weapons = 0;
    private void Awake()
    {
        if (m_gun_attachment)
        {
            current_weapon = m_gun_attachment.GetChild(current_weapon_index).GetComponent<WeaponSystem>();
            all_weapons = m_gun_attachment.childCount;

            for (int i = 0; i < all_weapons; i++)
            {
                if (i != current_weapon_index)
                {
                    m_gun_attachment.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    void SwitchWeapon(int direction)
    {
        current_weapon_index += direction;
        current_weapon_index = current_weapon_index % all_weapons;
        current_weapon.gameObject.SetActive(false);
        if (current_weapon_index < 0)
        {
            current_weapon_index = all_weapons - 1;
        }
        current_weapon = m_gun_attachment.GetChild(current_weapon_index).GetComponent<WeaponSystem>();
        current_weapon.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            SwitchWeapon(1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            SwitchWeapon(-1);
        }
        if (Input.GetButton("Fire1"))
        {
            if (current_weapon)
            {
                StartCoroutine(current_weapon.FireWeapon());
            }
        }
    }
}
