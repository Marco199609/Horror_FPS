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
        playerController.PlayerInspect.Inspect(transform, _rotateX, _rotateY, _rotateZ);
    }

    public string InteractableDescription()
    {
        return _inspectableDescription;
    }

    public bool NonInspectable()
    {
        return false;
    }

    public bool InspectableOnly()
    {
        return true;
    }

    public bool PassRotateX()
    {
        return _rotateX;
    }

    public bool PassRotateY()
    {
        return _rotateY;
    }

    public bool PassRotateZ()
    {
        return _rotateZ;
    }
}
