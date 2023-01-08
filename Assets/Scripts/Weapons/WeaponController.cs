using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject player;


    [Header("WeaponUI")]
    [SerializeField] private TextMeshProUGUI ammoText;




    [Header("Weapon Scripts")]
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private WeaponReload weaponReload;
    [SerializeField] private WeaponShoot weaponShoot;
    [SerializeField] private WeaponUI weaponUI;


    private void Awake()
    {
        weaponUI.UIUpdate(weaponData.currentAmmo, weaponData.reserveCapacity, ammoText);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            weaponShoot.Shoot(weaponData);
            weaponUI.UIUpdate(weaponData.currentAmmo, weaponData.reserveCapacity, ammoText);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            weaponReload.Reload(weaponData);
            weaponUI.UIUpdate(weaponData.currentAmmo, weaponData.reserveCapacity, ammoText);
        }

    }
}
