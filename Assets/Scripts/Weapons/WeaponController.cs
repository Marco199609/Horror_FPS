using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#region Components Required
[RequireComponent(typeof(WeaponReload))]
[RequireComponent(typeof(WeaponShoot))]
[RequireComponent(typeof(WeaponDamage))]
[RequireComponent(typeof(WeaponUI))]
[RequireComponent(typeof(WeaponSound))]
[RequireComponent(typeof(WeaponChange))]
#endregion

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponData currentWeaponData;

    [Header("Weapon Shoot")]
    [SerializeField] private Transform shootRayOrigin;

    [Header("Weapon UI")]
    [SerializeField] private TextMeshProUGUI ammoText;

    #region Weapon Scripts
    [Header("Weapon Scripts")]
    [SerializeField] private WeaponReload weaponReload;
    [SerializeField] private WeaponShoot weaponShoot;
    [SerializeField] private WeaponDamage weaponDamage;
    [SerializeField] private WeaponUI weaponUI;
    [SerializeField] private WeaponSound weaponSound;
    [SerializeField] private WeaponChange weaponChange;
    #endregion

    #region Weapon Input
    //Shows a header and instructions in the inspector
    [Header("Weapon Input", order = 1)]
    [Space(-10, order = 2)]
    [Header("Weapon Inputs are managed in the Input Manager.\nAttach the Input Manager here.", order = 3)]
    [Space(5, order = 4)]
    [SerializeField] private WeaponInput weaponInput;
    #endregion

    public bool isWeaponActive { get; private set; }
    //Available weapons; weapons[0] is weapon not active.
    [SerializeField] private GameObject[] weapons;

    private void Awake()
    {
        UIUpdate();
    }

    void Update()
    {
        CheckOrChangeActiveWeapons();

        if(isWeaponActive)
        {
            SendShootCommand();
            ReturnWeaponShootToOriginalState();
            SendReloadCommand();
        }
    }

    //Updates weapon UI
    private void UIUpdate()
    {
        weaponUI.UIUpdate(currentWeaponData.currentAmmo, currentWeaponData.reserveCapacity, ammoText);
    }


    private void SendShootCommand()
    {
        //Shoots weapon
        if (weaponInput.shootInput)
        {
            weaponShoot.Shoot(currentWeaponData, weaponDamage, shootRayOrigin, weaponSound);
            weaponShoot.readyToShoot = false;
            UIUpdate();
        }
    }

    private void ReturnWeaponShootToOriginalState()
    {
        //Returns WeaponShoot to it's original state
        if (weaponInput.leftMouseUpInput)
        {
            weaponShoot.readyToShoot = true;
            weaponShoot.FireRateCooldown = 0;
        }
    }

    private void SendReloadCommand()
    {
        //Reloads weapon and updates weapon UI
        if (weaponInput.reloadInput)
        {
            weaponReload.Reload(currentWeaponData);
            UIUpdate();
        }
    }

    private void CheckOrChangeActiveWeapons()
    {
        //Change weapons, and check if there is a weapon currently selected
        weaponChange.ChangeWeapon(weapons, weaponInput);
        currentWeaponData = weaponChange.currentWeaponData;
        isWeaponActive = currentWeaponData.isWeapon;
    }
}
