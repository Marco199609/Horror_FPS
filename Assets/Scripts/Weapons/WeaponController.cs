using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Shoot")]
    [SerializeField] private Transform shootRayOrigin;

    [Header("Weapon UI")]
    [SerializeField] private TextMeshProUGUI ammoText;

    [Header("Weapon Shoot Damage")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Weapon Scripts")]
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private WeaponReload weaponReload;
    [SerializeField] private WeaponShoot weaponShoot;
    [SerializeField] private WeaponDamage weaponDamage;
    [SerializeField] private WeaponUI weaponUI;


    private void Awake()
    {
        //Updates weapon UI
        weaponUI.UIUpdate(weaponData.currentAmmo, weaponData.reserveCapacity, ammoText);
    }

    void Update()
    {
        //Shoots weapon and updates weapon UI
        if(Input.GetMouseButton(0))
        {
            weaponShoot.Shoot(weaponData, weaponDamage, shootRayOrigin);
            weaponShoot.readyToShoot = false;
            weaponUI.UIUpdate(weaponData.currentAmmo, weaponData.reserveCapacity, ammoText);
        }

        //Reaturns WeaponShoot to it's original state
        if(Input.GetMouseButtonUp(0))
        {
            weaponShoot.readyToShoot = true;
            weaponShoot.FireRateCooldown = 0;
        }

        //Reloads weapon and updates weapon UI
        if(Input.GetKeyDown(KeyCode.R))
        {
            weaponReload.Reload(weaponData);
            weaponUI.UIUpdate(weaponData.currentAmmo, weaponData.reserveCapacity, ammoText);
        }
    }
}
