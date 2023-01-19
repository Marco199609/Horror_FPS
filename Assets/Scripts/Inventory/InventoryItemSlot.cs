using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{
    public Item Item;
    public ItemData ItemData;
    public Image SlotIcon;
    public GameObject RemoveItemButton;

    public void ItemBehaviourOnButtonClick()
    {
        ItemData.ItemBehaviour();
        ObjectManager.Instance.InventoryController.RemoveItem(this);
    }
}
