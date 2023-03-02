using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Inspectable : MonoBehaviour, IInteractable
{
    public string ActionDescription()
    {
        return "Inspect";
    }

    public void Interact(PlayerController playerController)
    {
        playerController.PlayerInspect.Inspect(transform);
    }

    public string InteractableDescription()
    {
        return "Letter";
    }
}
