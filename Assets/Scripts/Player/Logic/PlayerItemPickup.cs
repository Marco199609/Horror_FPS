using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class PlayerItemPickup : MonoBehaviour
{
    private ItemData _itemData;
    private ObjectManager objectManager;
    Ray ray;

    public void ItemPickup(PlayerInput playerInput, InventoryController inventoryController, PlayerData playerData)
    {
        RaycastHit hit;

        ray.origin = playerData.camHolder.position;
        ray.direction = playerData.camHolder.forward;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Item") && hit.distance <= playerData.itemPickupDistance) //Checks if ray hits item and reachable
            {
                ActivateUIPickupHand(playerData);

                if (playerInput.itemPickupInput) //Checks if player clicks mouse to pickup item
                    InteractWithItem(hit);
            }
            else
                DeactivatePickupHand(playerData);
        }
        else
            DeactivatePickupHand(playerData);
    }


    private void InteractWithItem(RaycastHit hit)
    {
        _itemData = hit.collider.GetComponent<ItemData>();

        ObjectManager.Instance.InventoryController.Add(_itemData.Item);
        Destroy(_itemData.gameObject); //gets item
    }

    private void ActivateUIPickupHand(PlayerData playerData)
    {
        playerData.UIPickupHand.gameObject.SetActive(true);
        playerData.UICenterPoint.gameObject.SetActive(false);
    }

    private void DeactivatePickupHand(PlayerData playerData)
    {
        playerData.UIPickupHand.gameObject.SetActive(false);
        playerData.UICenterPoint.gameObject.SetActive(true);
    }
}
