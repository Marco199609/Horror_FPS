using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReload : MonoBehaviour
{
    public void Reload(WeaponData weaponData)
    {
        if(weaponData.currentAmmo < weaponData.magazineCapacity)
        {
            if (weaponData.CurrentReserveCapacity >= (weaponData.magazineCapacity - weaponData.currentAmmo))
            {
                weaponData.CurrentReserveCapacity -= (weaponData.magazineCapacity - weaponData.currentAmmo);
                weaponData.currentAmmo += (weaponData.magazineCapacity - weaponData.currentAmmo);

                weaponData.reloadSound.Play();
            }
            else if (weaponData.CurrentReserveCapacity > 0)
            {
                weaponData.currentAmmo += weaponData.CurrentReserveCapacity;
                weaponData.CurrentReserveCapacity = 0;

                weaponData.reloadSound.Play();
            }

        }
    }
}
