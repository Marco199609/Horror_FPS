using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWeaponSlot : MonoBehaviour
{
    public GameObject Weapon;
    public WeaponData weaponData;
    public Image SlotIcon;
    public GameObject RemoveWeaponButton;

    public void ItemBehaviourOnButtonClick()
    {
        ObjectManager.Instance.InventoryController.RemoveWeapon(this);
    }
}
