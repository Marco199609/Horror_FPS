using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_InspectableOnly : MonoBehaviour, IInteractable
{
    [SerializeField] private string _inspectableDescription;
    
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
        return _inspectableDescription;
    }

    public bool NonInspectable()
    {
        return false;
    }
}
