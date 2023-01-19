using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUpdateSlots : MonoBehaviour
{
    public void UpdateSlots(List<InventoryItemSlot> _inventoryItemSlots, List<ItemData> itemDatas, 
        List<InventoryWeaponSlot> _inventoryWeaponSlots, GameObject[] weapons)
    {
        //UpdateItemSlots(_inventoryItemSlots, itemDatas);
        UpdateWeaponSlots(_inventoryWeaponSlots, weapons);
    }

    private void UpdateItemSlots(List<InventoryItemSlot> _inventoryItemSlots, List<ItemData> itemDatas)
    {
        for (int i = 0; i < _inventoryItemSlots.Count; i++)
        {
            if (i < itemDatas.Count)
            {
                _inventoryItemSlots[i].Item = itemDatas[i].Item; //Assigns this item to the inventory slot
                _inventoryItemSlots[i].ItemData = itemDatas[i]; //Assigns this items data to the inventory slot
                _inventoryItemSlots[i].SlotIcon.gameObject.SetActive(true); //Activates icon in slot
                _inventoryItemSlots[i].SlotIcon.GetComponent<Image>().sprite = itemDatas[i].Item.icon; //Sets item icon as the new slot icon
                _inventoryItemSlots[i].RemoveItemButton.SetActive(true); //Activates the remove button
                itemDatas[i].Item.CurrentInventorySlot = _inventoryItemSlots[i].GetComponent<InventoryItemSlot>(); //Sets the items current inventory slot for future use
            }
            else if (itemDatas.Count > 0)
            {
                _inventoryItemSlots[i].Item = null;
                _inventoryItemSlots[i].ItemData = null;
                _inventoryItemSlots[i].SlotIcon.gameObject.SetActive(false);
                _inventoryItemSlots[i].RemoveItemButton.SetActive(false);
            }
        }
    }

    private void UpdateWeaponSlots(List<InventoryWeaponSlot> _inventoryWeaponSlots, GameObject[] weapons)
    {
        for(int i = 0; i < weapons.Length; i++)
        {

            int a = i - 1;  //i - 1: weapons array is length 3; slots array is length 2. weapons[0] is reserved for no weapon

            if (i > 0) //i = 0 is reserved for no weapon
            {


                if (weapons[i] == null)
                {
                    _inventoryWeaponSlots[a].SlotIcon.gameObject.SetActive(false);
                    _inventoryWeaponSlots[a].Weapon = null;
                    _inventoryWeaponSlots[a].weaponData = null;
                    _inventoryWeaponSlots[a].RemoveWeaponButton.SetActive(false);
                }
                else
                {
                    _inventoryWeaponSlots[a].Weapon = weapons[i];
                    _inventoryWeaponSlots[a].weaponData = weapons[i].GetComponent<WeaponData>();
                    _inventoryWeaponSlots[a].SlotIcon.sprite = _inventoryWeaponSlots[a].weaponData.weaponIcon;
                    _inventoryWeaponSlots[a].SlotIcon.gameObject.SetActive(true);
                    _inventoryWeaponSlots[a].RemoveWeaponButton.SetActive(true);
                }

            }
        }
    }
}
