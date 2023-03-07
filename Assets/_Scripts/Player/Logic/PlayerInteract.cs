using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour, IPlayerInteract
{
    public void Interact(PlayerData playerData, RaycastHit hit, IPlayerInput playerInput, IPlayerInspect playerInspect)
    {
        if (hit.distance <= playerData.InteractDistance && hit.collider.GetComponent<IInteractable>() != null)
        {
            if (playerInput.playerPickupInput)
            {
                hit.collider.GetComponent<IInteractable>().Interact(GetComponent<PlayerController>());
            }
            else if (Input.GetMouseButtonDown(0) && hit.collider.GetComponent<IInteractable>().NonInspectable() == false)
            {
                playerInspect.Inspect(hit.transform);
            }
        }
    }
}
