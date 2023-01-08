using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReload : MonoBehaviour
{
    public void Reload(WeaponData weaponData)
    {
        if(weaponData.currentAmmo < weaponData.magazineCapacity)
        {
            if (weaponData.reserveCapacity >= (weaponData.magazineCapacity - weaponData.currentAmmo))
            {
                weaponData.reserveCapacity -= (weaponData.magazineCapacity - weaponData.currentAmmo);
                weaponData.currentAmmo += (weaponData.magazineCapacity - weaponData.currentAmmo);

                weaponData.reloadSound.Play();
            }
            else if (weaponData.reserveCapacity > 0)
            {
                weaponData.currentAmmo += weaponData.reserveCapacity;
                weaponData.reserveCapacity = 0;

                weaponData.reloadSound.Play();
            }
            else
                print("No reserve");

        }
    }
}
