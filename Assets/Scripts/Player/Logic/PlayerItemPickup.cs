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


        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Item")) //Checks if ray hits item
            {
                handIcon.SetActive(true);

                if (hit.distance <= playerData.itemPickupDistance) // checks if item reachable
                {
                    handIcon.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.3f); //Sets color green
                    if (playerInput.itemPickupInput)
                        Destroy(hit.collider.gameObject); //gets item
                }
                else
                    handIcon.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.3f); //checks if item unreachable and sets color red
            }
            else
                handIcon.SetActive(false);
        }
        else
            handIcon.SetActive(false);
    }
}
