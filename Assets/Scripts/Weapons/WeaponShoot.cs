using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public bool readyToShoot = true;

    public float FireRateCooldown;

    public void Shoot(WeaponData weaponData, WeaponDamage weaponDamage, Transform shootRayOrigin)
    {
        if(weaponData.isAuto)
        {
            if (weaponData.currentAmmo > 0)
            {
                if (FireRateCooldown <= 0)
                {
                    FireRateCooldown = weaponData.fireRate;

                    weaponDamage.DamageEnemy(weaponData.weaponDamage, weaponData.weaponRange, shootRayOrigin);
                    weaponData.currentAmmo--;

                    if (weaponData.shotSound.isPlaying)
                        weaponData.shotSound.Stop();
                    weaponData.shotSound.Play();
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
                    weaponDamage.DamageEnemy(weaponData.weaponDamage, weaponData.weaponRange, shootRayOrigin);

                    weaponData.currentAmmo--;

                    if (weaponData.shotSound.isPlaying)
                        weaponData.shotSound.Stop();
                    weaponData.shotSound.Play();
                }
                else
                    print("reload");
            }
        }






    }
}
