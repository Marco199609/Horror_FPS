using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [HideInInspector] public bool readyToShoot = true;
    [HideInInspector] public float FireRateCooldown;

    public void Shoot(WeaponData weaponData, WeaponDamage weaponDamage, Transform shootRayOrigin, WeaponSound weaponSound)
    {
        if (readyToShoot)
        {
            if (weaponData.Weapon.CurrentAmmo > 0)
            {
                weaponSound.ShootSound(weaponData);
                weaponDamage.DamageEnemy(weaponData.Weapon.WeaponDamage, weaponData.Weapon.WeaponRange, shootRayOrigin);
                weaponData.Weapon.CurrentAmmo--;
            }
        }
    }
}
