using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour, IWeaponDamage
{
    private void OnEnable()
    {
        WeaponShoot.WeaponShot += DamageEnemy;
    }

    private void OnDisable()
    {
        WeaponShoot.WeaponShot -= DamageEnemy;
    }


    public void DamageEnemy(WeaponData weaponData, EnemyData enemyData)
    {
        if(enemyData != null)
        {
            enemyData.EnemyHealth -= weaponData.Weapon.WeaponDamage;
            if (enemyData.EnemyHealth <= 0)
                Destroy(enemyData.gameObject);
        }
    }
}
