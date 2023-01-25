using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponShoot : MonoBehaviour, IWeaponShoot
{
    private bool _shoot = true; //Use when weapons isn't automatic
    //private float _fireRateCooldown; //Use when applying fire rate limit (eg. heavy revolvers)

    public bool DealDamage { get; private set; }
    public bool PlaySound { get; private set; }

    public void Shoot(WeaponInput weaponInput, WeaponData weaponData)
    {
        if (weaponInput.shootInput && _shoot)
        {
            if (weaponData.Weapon.CurrentAmmo > 0)
            {
                weaponData.Weapon.CurrentAmmo--;

                //Send commands to weapon controller
                DealDamage = true;
                PlaySound = true;

                _shoot = false;
            }
        }
        else
        {
            //Deactivate commands to weapon controller one frame after activating
            DealDamage = false;
            PlaySound = false;
        }

        if (weaponInput.leftMouseUpInput)
        {
            _shoot = true;
            //_fireRateCooldown = 0;//Use when applying fire rate limit (eg. heavy revolvers)
        }
    }

}
