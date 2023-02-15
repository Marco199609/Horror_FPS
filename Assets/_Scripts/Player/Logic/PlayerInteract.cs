using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour, IPlayerInteract
{
    private PlayerData _playerData;
    public void InteractWithObject(GameObject player, RaycastHit hit, IPlayerInput playerInput)
    {
        if (_playerData == null) _playerData = player.GetComponent<PlayerData>();

        if (hit.distance <= _playerData.itemPickupDistance && playerInput.playerPickupInput && 
            hit.collider.GetComponent<IInteractable>() != null) //Checks if item interactable and reachable
        {
            hit.collider.GetComponent<IInteractable>().Interact();
        }
    }
}
