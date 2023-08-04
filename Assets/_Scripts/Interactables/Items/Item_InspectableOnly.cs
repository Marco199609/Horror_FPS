using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_InspectableOnly : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _rotateX = true, _rotateY = true, _rotateZ = true;

    [SerializeField] private string _inspectableDescription;

    public string ActionDescription()
    {
        return "Inspect";
    }

    public void Interact(PlayerController playerController)
    {
        bool[] rotateXYZ = new bool[] { _rotateX, _rotateY, _rotateZ };
        playerController.PlayerInspect.Inspect(transform, rotateXYZ);
    }

    public string InteractableDescription()
    {
        return _inspectableDescription;
    }

    public bool[] InteractableType()
    {
        bool nonInspectable = false;
        bool inspectableOnly = true;

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
