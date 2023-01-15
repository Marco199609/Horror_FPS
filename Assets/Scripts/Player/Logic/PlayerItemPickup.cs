using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemPickup : MonoBehaviour
{
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
                ActivatePickupHand(playerData);

                if (playerInput.itemPickupInput)
                    Destroy(hit.collider.gameObject); //gets item
            }
            else
                DeactivatePickupHand(playerData);
        }
        else
            DeactivatePickupHand(playerData);
    }

    private void ActivatePickupHand(PlayerData playerData)
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
