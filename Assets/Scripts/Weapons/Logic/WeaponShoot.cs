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
        if(weaponData.isAuto)
        {
            if (weaponData.currentAmmo > 0)
            {
                if (FireRateCooldown <= 0)
                {
                    FireRateCooldown = weaponData.fireRate;

                    weaponSound.ShootSound(weaponData);
                    weaponDamage.DamageEnemy(weaponData.weaponDamage, weaponData.weaponRange, shootRayOrigin);
                    weaponData.currentAmmo--;
                }
                FireRateCooldown -= Time.deltaTime;
            }
            else
                print("reload");
        }
        else
        {
            if(readyToShoot)
            {
                if (weaponData.currentAmmo > 0)
                {
                    weaponSound.ShootSound(weaponData);
                    weaponDamage.DamageEnemy(weaponData.weaponDamage, weaponData.weaponRange, shootRayOrigin);
                    weaponData.currentAmmo--;
                }
                else
                    print("reload");
            }
        }
    }
}
