using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item_InventoryAddable : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _rotateX = true, _rotateY = true, _rotateZ = true;
    [SerializeField] private bool _nonInspectable;
    [SerializeField] private string _actionDescription, _interactableDescription;
    [SerializeField] private Vector3 _positionInInventory, _rotationInInventory, _scaleInInventory;

    public string ActionDescription()
    {
        return _actionDescription;
    }

    public void Interact(PlayerController playerController)
    {
        playerController.Inventory.Add(gameObject, _positionInInventory, _rotationInInventory, _scaleInInventory);
    }

    public string InteractableDescription()
    {
        return _interactableDescription;
    }

    public bool[] InteractableType()
    {
        bool nonInspectable = _nonInspectable;
        bool inspectableOnly = false;

        bool[] interactableType = new bool[] { nonInspectable, inspectableOnly };

        return interactableType;
    }

    public bool[] RotateXYZ()
    {
        bool[] rotateXYZ = new bool[] { _rotateX, _rotateY, _rotateZ };

        return rotateXYZ;
    }

    public void TriggerActions(ITriggerAction trigger, bool alreadyTriggered)
    {
        throw new System.NotImplementedException();
    }
}
