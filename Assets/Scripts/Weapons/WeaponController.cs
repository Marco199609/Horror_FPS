using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private bool isWeaponActive;

    [SerializeField] private WeaponData currentWeaponData;

    [Header("Weapon Shoot")]
    [SerializeField] private Transform shootRayOrigin;

    [Header("Weapon UI")]
    [SerializeField] private TextMeshProUGUI ammoText;

    [Header("Weapon Shoot Damage")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Weapon Scripts")]
    [SerializeField] private WeaponReload weaponReload;
    [SerializeField] private WeaponShoot weaponShoot;
    [SerializeField] private WeaponDamage weaponDamage;
    [SerializeField] private WeaponUI weaponUI;
    [SerializeField] private WeaponSound weaponSound;
    [SerializeField] private WeaponChange weaponChange;
    [SerializeField] private WeaponInput weaponInput;

    [SerializeField] private GameObject[] weapons;

    private void Awake()
    {
        //Updates weapon UI
        weaponUI.UIUpdate(currentWeaponData.currentAmmo, currentWeaponData.reserveCapacity, ammoText);
    }

    void Update()
    {
        //Shoots weapon and updates weapon UI
        if(Input.GetMouseButton(0))
        {
            weaponShoot.Shoot(currentWeaponData, weaponDamage, shootRayOrigin, weaponSound);
            weaponShoot.readyToShoot = false;
            weaponUI.UIUpdate(currentWeaponData.currentAmmo, currentWeaponData.reserveCapacity, ammoText);
        }

        //Returns WeaponShoot to it's original state
        if(Input.GetMouseButtonUp(0)) 
        {
            weaponShoot.readyToShoot = true;
            weaponShoot.FireRateCooldown = 0;
        }

        //Reloads weapon and updates weapon UI
        if(Input.GetKeyDown(KeyCode.R))
        {
            weaponReload.Reload(currentWeaponData);
            weaponUI.UIUpdate(currentWeaponData.currentAmmo, currentWeaponData.reserveCapacity, ammoText);
        }

        if (Input.anyKeyDown)
            print(Input.inputString);
    }
}
