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

    private void PlaceWeaponOnHolder(WeaponData _weaponData)
    {
        if (_weaponGeneralData == null) _weaponGeneralData = ObjectManager.Instance.WeaponGeneralData;

        if (_weaponData.GetComponent<Collider>() != false) _weaponData.GetComponent<Collider>().enabled = false;

        //Place weapon in weapon holder
        _weaponData.transform.SetParent(_weaponGeneralData.transform);
        _weaponData.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));
        _weaponData.gameObject.layer = 6; // 6 is weapon layer
        _weaponData.gameObject.SetActive(false);
    }
}
