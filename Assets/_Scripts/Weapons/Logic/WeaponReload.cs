using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class WeaponReload : MonoBehaviour
{
    private bool _reload, _checkAmmo;
    private float _timer, _defaultTimer = 0.2f;

    private void Awake()
    {
        _timer = _defaultTimer;
    }

    public void Reload(WeaponGeneralData weaponGeneralData, WeaponData currentWeaponData, WeaponInput weaponInput)
    {
        CheckAmmo(weaponGeneralData, currentWeaponData, weaponInput);
    }

    private void CheckAmmo(WeaponGeneralData weaponGeneralData, WeaponData currentWeaponData, WeaponInput weaponInput)
    {
        if(weaponInput.reloadInput)
        {
            _timer -= Time.deltaTime;

            if(_timer > 0)
            {
                _checkAmmo = false;
                _reload = true;      //If timer not 0, sets command to reload
            }
            else if (_timer <= 0)
            {
                _checkAmmo = true;  //If player keeps key down for a certain amount of time, change the command to check ammo
                _reload = false;
                weaponGeneralData.AmmoUI.SetActive(true);
            }
        }

        if (!weaponInput.reloadInput && _checkAmmo)
        {
            _checkAmmo = false; //Deactivates ammo UI when the player releases key
            _timer = _defaultTimer;
            weaponGeneralData.AmmoUI.SetActive(false);
        }

        if(!weaponInput.reloadInput && _reload) //Reload is reset in apply reload
        {
            _timer = _defaultTimer;
            ApplyReload(currentWeaponData);
        }
    }

    private void ApplyReload(WeaponData currentWeaponData)
    {
        Weapon weapon = currentWeaponData.Weapon;

        if (weapon.CurrentAmmo < weapon.MagazineCapacity)
        {
            if (weapon.CurrentReserveCapacity >= (weapon.MagazineCapacity - weapon.CurrentAmmo))
            {
                weapon.CurrentReserveCapacity -= (weapon.MagazineCapacity - weapon.CurrentAmmo);
                weapon.CurrentAmmo += (weapon.MagazineCapacity - weapon.CurrentAmmo);

                currentWeaponData.ReloadSound.Play();
            }
            else if (weapon.CurrentReserveCapacity > 0)
            {
                weapon.CurrentAmmo += weapon.CurrentReserveCapacity;
                weapon.CurrentReserveCapacity = 0;

                weapon.ReloadSound.Play();
            }

            _reload = false;
        }
    }
}
