using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    Ray ray;

    public void DamageEnemy(int weaponDamage, int weaponRange, Transform shootRayOrigin)
    {
        RaycastHit hit;

        ray.origin = shootRayOrigin.position;
        ray.direction = shootRayOrigin.transform.forward;


        if (Physics.Raycast(ray, out hit, weaponRange))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyData>().EnemyHealth -= weaponDamage;

                if (hit.collider.GetComponent<EnemyData>().EnemyHealth <= 0)
                    Destroy(hit.collider.gameObject);
            }
        }
    }
}
