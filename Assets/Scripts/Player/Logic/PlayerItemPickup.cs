using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemPickup : MonoBehaviour
{
    private ItemData _itemData;

    public void ItemPickup(RaycastHit hit, PlayerInput playerInput)
    {
        if (playerInput.itemPickupInput) //Checks if player clicks mouse to pickup item
        {
            _itemData = hit.collider.GetComponent<ItemData>();

            ObjectManager.Instance.InventoryController.Add(_itemData.Item);
            Destroy(_itemData.gameObject); //gets item
        }
    }
}
