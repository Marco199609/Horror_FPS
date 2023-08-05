using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item_Energy : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _rotateX = true, _rotateY = true, _rotateZ = true;
    [SerializeField] private bool _nonInspectable;
    [SerializeField] private string _interactableDescription, _actionDescription;

    public string InteractableDescription()
    {
        return _interactableDescription;
    }
    public string ActionDescription()
    {
        return _actionDescription;
    }
    public void Interact(PlayerController playerController)
    {
        playerController.PlayerFlashlight.AddBattery();
        Destroy(gameObject);
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

    public bool TriggerActions(ITriggerAction trigger, bool alreadyTriggered, float triggerDelay)
    {
        throw new System.NotImplementedException();
    }
}
