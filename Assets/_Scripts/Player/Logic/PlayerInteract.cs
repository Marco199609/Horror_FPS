using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInteract
{
    void Interact(PlayerData playerData, RaycastHit hit, IPlayerInput playerInput, IPlayerInspect playerInspect);
}

public class PlayerInteract : MonoBehaviour, IPlayerInteract
{
    public void Interact(PlayerData playerData, RaycastHit hit, IPlayerInput playerInput, IPlayerInspect playerInspect)
    {
        if (hit.distance <= playerData.InteractDistance && hit.collider.GetComponent<IInteractable>() != null)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (playerInput.playerPickupInput)
            {
                interactable.Interact(GetComponent<PlayerController>());
                interactable.TriggerActions();
            }
            else if (Input.GetMouseButtonDown(0) && hit.collider.GetComponent<IInteractable>().InteractableType()[0] == false) //index 0 is NonInspectable
            {
                playerInspect.Inspect(hit.transform, interactable.RotateXYZ());
                interactable.TriggerActions();
            }
        }
    }
}
