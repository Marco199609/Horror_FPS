using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponPickup : MonoBehaviour
{
    public void WeaponPickup(RaycastHit hit, PlayerInput playerInput)
    {
        if (playerInput.playerPickupInput && hit.collider.CompareTag("Weapon")) //Checks if player clicks mouse to pickup weapon
        {

            WeaponData _weaponData = hit.collider.GetComponent<WeaponData>();
            ObjectManager.Instance.InventoryController.AddWeapon(_weaponData);

            PlaceWeaponOnHolder(_weaponData);
        }
    }

    private void PlaceWeaponOnHolder(WeaponData _weaponData)
    {
        if (_weaponData.GetComponent<Collider>() != false)
            _weaponData.GetComponent<Collider>().enabled = false;

        _weaponData.transform.SetParent(ObjectManager.Instance.WeaponGeneralData.transform);

        //Place weapon in weapon holder
    }
}
