using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemPickup : MonoBehaviour
{
    Ray ray;

    public void ItemPickup(PlayerInput playerInput, InventoryController inventoryController, PlayerData playerData, GameObject handIcon)
    {
        RaycastHit hit;

        ray.origin = playerData.camHolder.position;
        ray.direction = playerData.camHolder.forward;


        if (Physics.Raycast(ray, out hit, playerData.itemPickupDistance))
        {
            if (hit.collider.CompareTag("Item"))
            {
                handIcon.SetActive(true);
                if (playerInput.itemPickupInput)
                    Destroy(hit.collider.gameObject);
            }
            else
                handIcon.SetActive(false);
        }
        else
            handIcon.SetActive(false);

    }
}
