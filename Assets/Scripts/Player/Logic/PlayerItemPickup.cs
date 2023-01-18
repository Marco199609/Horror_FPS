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

            ObjectManager.Instance.InventoryController.Add(_itemData, _itemData.Item);

            DestroyItem();
        }
    }

    private void DestroyItem()
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
