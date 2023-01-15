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
    private ObjectManager objectManager;

    //Objects in object manager
    private WeaponInput weaponInput;
    private WeaponGeneralData weaponGeneralData;

    //Used in weapon change script
    private WeaponData currentWeaponData;

    //Objects in weapon general data
    private Transform shootRayOrigin;
    private GameObject weaponUICanvas;
    private TextMeshProUGUI UIAmmoText;

    //Weapon scripts
    private WeaponReload weaponReload;
    private WeaponShoot weaponShoot;
    private WeaponDamage weaponDamage;
    private WeaponUI weaponUI;
    private WeaponSound weaponSound;
    private WeaponChange weaponChange;

    public bool isWeaponActive { get; private set; }
    private bool _firstUIUpdate;

    //Available weapons in weapon general data
    private GameObject[] weaponsAvailable;

    private void Awake()
    {
        weaponReload = GetComponent<WeaponReload>();
        weaponShoot = GetComponent<WeaponShoot>();
        weaponDamage = GetComponent<WeaponDamage>();
        weaponUI = GetComponent<WeaponUI>();
        weaponSound = GetComponent<WeaponSound>();
        weaponChange = GetComponent<WeaponChange>();

        //Adds this object to object manager for future use
        ObjectManager.Instance.WeaponController = this;
    }

    private void Start()
    {
        objectManager = ObjectManager.Instance;
        weaponInput = objectManager.WeaponInput;
        weaponGeneralData = objectManager.WeaponGeneralData;

        weaponsAvailable = weaponGeneralData.WeaponsAvailable;

        shootRayOrigin = weaponGeneralData.shootRayOrigin;
        weaponUICanvas = weaponGeneralData.weaponUICanvas;
        UIAmmoText = weaponGeneralData.ammoText;
    }

    void Update()
    {
        //Controls weapons only if invetory disabled
        if (!objectManager.InventoryController.IsInventoryEnabled)
        {
            CheckOrChangeActiveWeapons();

            if (isWeaponActive)
            {
                SendShootCommand();
                ReturnWeaponShootToOriginalState();
                SendReloadCommand();

                if (!weaponUICanvas.activeInHierarchy)
                    weaponUICanvas.SetActive(true);

                if (!_firstUIUpdate)
                {
                    UIUpdate();
                    _firstUIUpdate = true;
                }

                if (weaponInput.LeftMouseDownInput || weaponInput.reloadInput)
                    UIUpdate();
            }
            else
            {
                weaponUICanvas.SetActive(false);
            }
        }
        else
            weaponUICanvas.SetActive(false); //Deactivates weapon UI if inventory enabled
    }

    //Updates weapon UI
    //For loop in this method. Do no run more than once
    private void UIUpdate()
    {
        weaponUI.UIUpdate(weaponGeneralData, currentWeaponData.currentAmmo, currentWeaponData.reserveCapacity, UIAmmoText);
    }


    private void SendShootCommand()
    {
        //Shoots weapon
        if (weaponInput.shootInput)
        {
            weaponShoot.Shoot(currentWeaponData, weaponDamage, shootRayOrigin, weaponSound);
            weaponShoot.readyToShoot = false;
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
        }
    }

    private void CheckOrChangeActiveWeapons()
    {
        //Change weapons, and check if there is a weapon currently selected
        weaponChange.ChangeWeapon(weaponsAvailable, weaponInput);
        currentWeaponData = weaponChange.currentWeaponData;
        isWeaponActive = currentWeaponData.isWeapon;
    }
}
