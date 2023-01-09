using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform shootRayOrigin;


    [Header("WeaponUI")]
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
        weaponUI.UIUpdate(weaponData.currentAmmo, weaponData.reserveCapacity, ammoText);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            weaponShoot.Shoot(weaponData, weaponDamage, shootRayOrigin);
            weaponShoot.readyToShoot = false;
            weaponUI.UIUpdate(weaponData.currentAmmo, weaponData.reserveCapacity, ammoText);
        }

        if(Input.GetMouseButtonUp(0))
        {
            weaponShoot.readyToShoot = true;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            weaponReload.Reload(weaponData);
            weaponUI.UIUpdate(weaponData.currentAmmo, weaponData.reserveCapacity, ammoText);
        }

    }
}
