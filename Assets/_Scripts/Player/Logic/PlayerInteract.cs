using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour, IPlayerInteract
{
    public void Interact(PlayerData playerData, RaycastHit hit, IPlayerInput playerInput)
    {
        if (hit.distance <= playerData.InteractDistance && playerInput.playerPickupInput && hit.collider.GetComponent<IInteractable>() != null)
        {
            hit.collider.GetComponent<IInteractable>().Interact(GetComponent<PlayerController>());
        }
    }
}
