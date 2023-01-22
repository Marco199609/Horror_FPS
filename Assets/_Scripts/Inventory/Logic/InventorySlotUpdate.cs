using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUpdate : MonoBehaviour, IInventorySlotUpdate
{
    private List<ItemData> _itemDatas;
    private GameObject[] _weapons;
    public void UpdateSlots(InventoryController inventoryController, List<InventoryItemSlot> inventoryItemSlots, List<InventoryWeaponSlot> inventoryWeaponSlots)
    {
        if (_itemDatas == null) _itemDatas = inventoryController.itemDatas;
        if (_weapons == null) _weapons = inventoryController.weapons;


        UpdateItemSlots(inventoryItemSlots);
        UpdateWeaponSlots(inventoryWeaponSlots, _weapons);
    }

    private void UpdateItemSlots(List<InventoryItemSlot> inventoryItemSlots)
    {
        for (int i = 0; i < inventoryItemSlots.Count; i++)
        {
            if (i < _itemDatas.Count)
            {
                inventoryItemSlots[i].Item = _itemDatas[i].Item; //Assigns this item to the inventory slot
                inventoryItemSlots[i].ItemData = _itemDatas[i]; //Assigns this items data to the inventory slot
                inventoryItemSlots[i].SlotIcon.gameObject.SetActive(true); //Activates icon in slot
                inventoryItemSlots[i].SlotIcon.GetComponent<Image>().sprite = _itemDatas[i].Item.icon; //Sets item icon as the new slot icon
                inventoryItemSlots[i].RemoveItemButton.SetActive(true); //Activates the remove button
                _itemDatas[i].Item.CurrentInventorySlot = inventoryItemSlots[i].GetComponent<InventoryItemSlot>(); //Sets the items current inventory slot for future use
            }
            else// if (_itemDatas.Count > 0)
            {
                inventoryItemSlots[i].Item = null;
                inventoryItemSlots[i].ItemData = null;
                inventoryItemSlots[i].SlotIcon.gameObject.SetActive(false);
                inventoryItemSlots[i].RemoveItemButton.SetActive(false);
            }
        }
    }

    private void UpdateWeaponSlots(List<InventoryWeaponSlot> _inventoryWeaponSlots, GameObject[] weapons)
    {
        for (int i = 0; i < weapons.Length; i++)
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
                    _inventoryWeaponSlots[a].SlotIcon.sprite = _inventoryWeaponSlots[a].weaponData.inventoryIcon;
                    _inventoryWeaponSlots[a].SlotIcon.gameObject.SetActive(true);
                    _inventoryWeaponSlots[a].RemoveWeaponButton.SetActive(true);
                }

            }
        }
    }
}
