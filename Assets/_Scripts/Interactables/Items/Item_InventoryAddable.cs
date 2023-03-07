using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_InventoryAddable : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _nonInspectable;
    [SerializeField] private string _actionDescription, _interactableDescription;
    [SerializeField] private Vector3 _positionInInventory, _rotationInInventory;

    public string ActionDescription()
    {
        return _actionDescription;
    }

    public void Interact(PlayerController playerController)
    {
        playerController.Inventory.Add(gameObject, _positionInInventory, _rotationInInventory);
    }

    public string InteractableDescription()
    {
        return _interactableDescription;
    }

    public bool NonInspectable()
    {
        return _nonInspectable;
    }

    public bool InspectableOnly()
    {
        return false;
    }
}
