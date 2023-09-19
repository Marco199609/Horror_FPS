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

            if (playerInput.playerInteractInput)
            {
                interactable.Interact(GetComponent<PlayerController>(), true, false);
                //interactable.TriggerActions();
            }
            else if (Input.GetMouseButtonDown(0) && hit.collider.GetComponent<IInteractable>().InteractableIsNonInspectableOrInspectableOnly()[0] == false) //index 0 is NonInspectable
            {
                interactable.Interact(GetComponent<PlayerController>(), false, true);
                playerInspect.Inspect(hit.transform, interactable.RotateXYZ());
                //interactable.TriggerActions();
            }
        }
    }
}
