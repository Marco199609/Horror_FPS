using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Health : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _rotateX = true, _rotateY = true, _rotateZ = true;
    [SerializeField] private bool _nonInspectable;
    [SerializeField] private string _description;
    [SerializeField] private int _health;


    public string InteractableDescription()
    {
        return _description;
    }

    public string ActionDescription()
    {
        return "Take";
    }

    public void Interact(PlayerController playerController)
    {
        Behaviour();
    }
    public void Behaviour()
    {
        //ObjectManager.Instance.Player.GetComponent<Health>().ModifyHealth(_health);
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
