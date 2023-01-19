using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemPickup : MonoBehaviour, IPlayerPickup
{
    private PlayerData _playerData;
    public void Pickup(GameObject player, RaycastHit hit, PlayerInput playerInput)
    {
        if (_playerData == null) _playerData = player.GetComponent<PlayerData>();

        if (hit.distance <= _playerData.itemPickupDistance && playerInput.playerPickupInput && 
            hit.collider.CompareTag("Item")) //Checks if player clicks mouse to pickup item and item reachable
        {
            ItemData itemData = hit.collider.GetComponent<ItemData>();
            ObjectManager.Instance.InventoryController.AddItem(itemData);

            DestroyItem(itemData);
        }
    }

    private void DestroyItem(ItemData itemData)
    {
        //Disables components instead of destroying the object, so that the item behaviour script works in the inventory slot
        if (itemData.GetComponent<MeshRenderer>() != null)
            itemData.GetComponent<MeshRenderer>().enabled = false;
        if (itemData.GetComponent<Collider>() != false)
            itemData.GetComponent<Collider>().enabled = false;

        //Destroys item children, if any
        int childs = itemData.transform.childCount;
        for (int i = 0; i < childs; i++)
        {
            Destroy(itemData.transform.GetChild(i).gameObject);
        }
    }
}
