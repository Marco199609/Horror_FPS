using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponShoot : MonoBehaviour, IWeaponShoot
{
    private bool _shoot = true; //Use when weapons isn't automatic
    //private float _fireRateCooldown; //Use when applying fire rate limit (eg. heavy revolvers)

    public delegate void WeaponShootDelegate(WeaponData currentWeaponData, EnemyData enemyData);
    public static WeaponShootDelegate WeaponShot;

    public void Shoot(WeaponInput weaponInput, WeaponData weaponData, EnemyData enemyData)
    {
        if (weaponInput.shootInput && _shoot)
        {
            if (weaponData.Weapon.CurrentAmmo > 0)
            {
                weaponData.Weapon.CurrentAmmo--;

                //Send event to weapon damage and weapon sound scripts
                WeaponShot?.Invoke(weaponData, enemyData);

                _shoot = false;
            }
        }

        if (weaponInput.leftMouseUpInput)
        {
            _shoot = true;
            //_fireRateCooldown = 0;//Use when applying fire rate limit (eg. heavy revolvers)
        }
    }

}
