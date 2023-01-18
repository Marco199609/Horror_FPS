using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item Item;
    public ItemData ItemData;
    public Image SlotIcon;
    public GameObject RemoveItemButton;

    public void ItemBehaviourOnButtonClick()
    {
        ItemData.ItemBehaviour();
        ObjectManager.Instance.InventoryController.Remove(this);
    }
}
