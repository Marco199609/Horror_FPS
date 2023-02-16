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
[RequireComponent(typeof(WeaponAim))]
[RequireComponent(typeof(WeaponAnimations))]
#endregion

public class WeaponController : MonoBehaviour
{
    private ObjectManager _objectManager;
    private WeaponInput _weaponInput;
    private WeaponGeneralData _weaponGeneralData;

    private WeaponData _currentWeaponData; //Set in weapon change script

    //Weapon scripts
    private IWeaponReload _weaponReload;
    private IWeaponShoot _weaponShoot;
    private IWeaponDamage _weaponDamage;
    private IWeaponUI _weaponUI;
    private IWeaponSound _weaponSound;
    private IWeaponChange _weaponChange;
    private IWeaponAim _weaponAim;
    private IWeaponAnimations _weaponAnimations;

    public bool IsWeaponActive { get; private set; }

    private Ray _ray; //Used in weapon damage and crosshair control
    private GameObject[] _weaponsAvailable; //Available weapons in weapon general data

    private void Awake()
    {
        _weaponReload = GetComponent<IWeaponReload>();
        _weaponShoot = GetComponent<IWeaponShoot>();
        _weaponDamage = GetComponent<IWeaponDamage>();
        _weaponUI = GetComponent<IWeaponUI>();
        _weaponSound = GetComponent<IWeaponSound>();
        _weaponChange = GetComponent<IWeaponChange>();
        _weaponAim = GetComponent<IWeaponAim>();
        _weaponAnimations = GetComponent<IWeaponAnimations>();

        //Adds this object to object manager for future use
        ObjectManager.Instance.WeaponController = this;
    }

    private void Start()
    {
        _objectManager = ObjectManager.Instance;
        _weaponInput = _objectManager.WeaponInput;
        _weaponGeneralData = _objectManager.WeaponGeneralData;
        _weaponsAvailable = _weaponGeneralData.WeaponsAvailable;
    }

    void Update()
    {
        CheckOrChangeActiveWeapons(); //Weapon change can be done even if inventory open
        UIUpdate(); //Place this after CheckOrChangeWeapons

        //Controls weapons only if inventory disabled
        if (!_objectManager.InventoryController.IsInventoryEnabled && IsWeaponActive)
        {
            SendShootCommand();
            WeaponCrosshairAndDamage();
            WeaponSound();
            SendReloadCommand();
            AimWeapon();
        }
    }

    //Updates weapon UI
    //For loop in this method. Do no run more than once
    private void UIUpdate()
    {
        _weaponUI.UIUpdate(_currentWeaponData, this);
    }

    private void AimWeapon()
    {
        _weaponAim.Aim(_currentWeaponData, _weaponInput);
    }

    private void SendShootCommand()
    {
        _weaponShoot.Shoot(_weaponInput, _currentWeaponData);
    }

    private void SendReloadCommand()
    {
        //Reloads weapon and updates weapon UI
        _weaponReload.Reload(_currentWeaponData, _weaponInput);
    }

    private void WeaponCrosshairAndDamage()
    {
        bool enemyInRange;

        RaycastHit hit;
        _ray.origin = _weaponGeneralData.ShootRayOrigin.position;
        _ray.direction = _weaponGeneralData.ShootRayOrigin.forward;

        if (Physics.Raycast(_ray, out hit, _currentWeaponData.Weapon.WeaponRange) && hit.collider.CompareTag("Enemy"))
        {
            enemyInRange = true;

            //Place this after the shoot command
            if (_weaponShoot.DealDamage) _weaponDamage.DamageEnemy(_currentWeaponData, hit);
        }
        else 
            enemyInRange = false;

        _weaponUI.CrosshairColorUpdate(enemyInRange);
    }

    private void WeaponSound()
    {
        if(_weaponShoot.PlaySound) _weaponSound.ShootSound(_currentWeaponData);
    }

    private void CheckOrChangeActiveWeapons()
    {
        //Change weapons, and check if there is a weapon currently selected
        _weaponChange.ChangeWeapon(_weaponsAvailable, _weaponInput);
        _currentWeaponData = _weaponChange.currentWeaponData;
        IsWeaponActive = _currentWeaponData.Weapon.IsWeapon;
    }
}