using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponPickup : MonoBehaviour, IPlayerPickup
{
    private WeaponGeneralData _weaponGeneralData;
    private PlayerData _playerData;
    public void Pickup(GameObject player, RaycastHit hit, PlayerInput playerInput)
    {
        if (_playerData == null) _playerData = player.GetComponent<PlayerData>();

        if (hit.distance <= _playerData.itemPickupDistance && playerInput.playerPickupInput && hit.collider.CompareTag("Weapon")) //Checks if player clicks mouse to pickup weapon
        {
            WeaponData _weaponData = hit.collider.GetComponent<WeaponData>();
            ObjectManager.Instance.InventoryController.AddWeapon(_weaponData);

            PlaceWeaponOnHolder(_weaponData);
        }
    }

    private void PlaceWeaponOnHolder(WeaponData weaponData)
    {
        if (_weaponGeneralData == null) _weaponGeneralData = ObjectManager.Instance.WeaponGeneralData;

        if (weaponData.GetComponent<Collider>() != false) weaponData.GetComponent<Collider>().enabled = false;

        //Place weapon in weapon holder
        weaponData.transform.SetParent(_weaponGeneralData.transform);
        weaponData.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));
        weaponData.gameObject.SetActive(false);

        ChangeWeaponLayer(weaponData);
    }

    private void ChangeWeaponLayer(WeaponData weaponData)
    {
        weaponData.gameObject.layer = 6; // 6 is weapon layer
        int childs = weaponData.transform.childCount;
        for (int i = 0; i < childs; i++)
        {
            weaponData.transform.GetChild(i).gameObject.layer = 6;
        }
    }
}
