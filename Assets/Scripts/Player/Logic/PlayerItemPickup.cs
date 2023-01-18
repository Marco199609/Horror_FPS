using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemPickup : MonoBehaviour
{
    public void ItemPickup(RaycastHit hit, PlayerInput playerInput)
    {
        if (playerInput.itemPickupInput) //Checks if player clicks mouse to pickup item
        {
            ItemData _itemData = hit.collider.GetComponent<ItemData>();

            ObjectManager.Instance.InventoryController.Add(_itemData);

            DestroyItem(_itemData);
        }
    }

    private void DestroyItem(ItemData _itemData)
    {
        //Disables components instead of destroying the object, so that the item behaviour script works in the inventory slot
        if (_itemData.GetComponent<MeshRenderer>() != null)
            _itemData.GetComponent<MeshRenderer>().enabled = false;
        if (_itemData.GetComponent<Collider>() != false)
            _itemData.GetComponent<Collider>().enabled = false;

        //Destroys item children, if any
        int childs = _itemData.transform.childCount;
        for(int i = 0; i < childs; i++)
        {
            Destroy(_itemData.transform.GetChild(i).gameObject);
        }
    }
}
