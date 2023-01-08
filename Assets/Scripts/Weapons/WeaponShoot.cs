using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public void Shoot(WeaponData weaponData)
    {
        if (weaponData.currentAmmo > 0)
        {
            weaponData.currentAmmo--;

            if (weaponData.shotSound.isPlaying)
                weaponData.shotSound.Stop();
            weaponData.shotSound.Play();
        }
        else
            print("reload");
    }
}
