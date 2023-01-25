using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour, IWeaponDamage
{
    public void DamageEnemy(WeaponData weaponData, RaycastHit hit)
    {
        hit.collider.GetComponent<EnemyData>().EnemyHealth -= weaponData.Weapon.WeaponDamage;

        if (hit.collider.GetComponent<EnemyData>().EnemyHealth <= 0)
            Destroy(hit.collider.gameObject);
    }
}
