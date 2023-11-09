using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform m_muzzle;
    [SerializeField] private float m_fire_rate = 0.25f; 
    
    [SerializeField] private float m_max_spread_angle = 45f;
    [SerializeField] [Range(1, 50)] private int m_max_projectiles = 1;
    [SerializeField] private float m_damage = 20f;

    private bool can_fire = true;
    
    public IEnumerator FireWeapon() {
        if (can_fire) {
            can_fire = false;
            CalculateFireDirection();
            yield return new WaitForSecondsRealtime(m_fire_rate);
            can_fire = true;
        }
    }

    private void CalculateFireDirection() {
        if (m_muzzle && projectile) {
            if (projectile.GetComponent<ProjectileLogic>()) {
                for (int i = 0; i < m_max_projectiles; i++) {
                    GameObject projectile_inst = Instantiate(projectile);
                    Vector3 dir = m_muzzle.forward;
                    Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Plane p = new Plane( Vector3.up, transform.position );
                    if( p.Raycast( mouseRay, out float hitDist) ){
                        Vector3 hitPoint = mouseRay.GetPoint( hitDist );
                        dir = (hitPoint - m_muzzle.position).normalized;
                    }
                    Quaternion spread_rotation = Quaternion.Euler(0, Random.Range(-m_max_spread_angle, m_max_spread_angle), 0);
                    // Apply the spread rotation to the original direction
                    Vector3 out_dir = spread_rotation * dir;

                    projectile_inst.GetComponent<ProjectileLogic>().SetupProjectile(out_dir, m_muzzle.position, m_damage);
                }
            }
        }
    }
}
