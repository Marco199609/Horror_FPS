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

    public bool NonInspectable()
    {
        return _nonInspectable;
    }
    public bool InspectableOnly()
    {
        return false;
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
