using System.Collections.Generic;
using UnityEngine;

public interface IInventorySlotUpdate
{
    void UpdateSlots(InventoryController inventoryController, List<InventoryItemSlot> inventoryItemSlots, List<InventoryWeaponSlot> inventoryWeaponSlots);
}