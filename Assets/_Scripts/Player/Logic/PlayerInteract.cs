using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour, IPlayerInteract
{
    public void Interact(PlayerData playerData, RaycastHit hit, IPlayerInput playerInput)
    {
        if (hit.distance <= playerData.itemPickupDistance && playerInput.playerPickupInput && 
            hit.collider.GetComponent<IInteractable>() != null) //Checks if item interactable and reachable
        {
            hit.collider.GetComponent<IInteractable>().Interact();
        }
    }
}
