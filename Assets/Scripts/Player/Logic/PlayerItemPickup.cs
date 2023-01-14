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
            if (hit.collider.CompareTag("Item")) //Checks if ray hits item
            {
                ActivatePickupHand(playerData);

                if (hit.distance <= playerData.itemPickupDistance) // checks if item reachable
                {
                    playerData.UIPickupHand.color = new Color(0, 1, 0, 0.7f); //Sets color green
                    if (playerInput.itemPickupInput)
                        Destroy(hit.collider.gameObject); //gets item
                }
                else
                    playerData.UIPickupHand.color = new Color(1, 0, 0, 0.5f); //checks if item unreachable and sets color red
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
