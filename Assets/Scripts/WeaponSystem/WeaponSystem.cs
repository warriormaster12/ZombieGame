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
                GameObject projectile_inst = Instantiate(projectile);
                Vector3 dir = m_muzzle.forward;
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane p = new Plane( Vector3.up, transform.position );
                if( p.Raycast( mouseRay, out float hitDist) ){
                    Vector3 hitPoint = mouseRay.GetPoint( hitDist );
                    dir = (hitPoint - m_muzzle.position).normalized;
                }
                projectile_inst.GetComponent<ProjectileLogic>().SetupProjectile(dir, m_muzzle.position);
            }
        }
    }
}
