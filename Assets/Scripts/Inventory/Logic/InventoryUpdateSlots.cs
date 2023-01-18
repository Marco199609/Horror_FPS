using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUpdateSlots : MonoBehaviour
{
    public void UpdateSlots(List<InventorySlot> _inventorySlots, List<Item> items, List<ItemData> itemDatas)
    {
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            if (i < items.Count)
            {
                _inventorySlots[i].Item = items[i]; //Assigns this item to the inventory slot
                _inventorySlots[i].ItemData = itemDatas[i]; //Assigns this items data to the inventory slot
                _inventorySlots[i].SlotIcon.gameObject.SetActive(true); //Activates icon in slot
                _inventorySlots[i].SlotIcon.GetComponent<Image>().sprite = items[i].icon; //Sets item icon as the new slot icon
                _inventorySlots[i].RemoveItemButton.SetActive(true); //Activates the remove button
                items[i].CurrentInventorySlot = _inventorySlots[i].GetComponent<InventorySlot>(); //Sets the items current inventory slot for future use
            }
            else
            {
                _inventorySlots[i].Item = null;
                _inventorySlots[i].ItemData = null;
                _inventorySlots[i].SlotIcon.gameObject.SetActive(false);
                _inventorySlots[i].RemoveItemButton.SetActive(false);
            }
        }
    }
}
