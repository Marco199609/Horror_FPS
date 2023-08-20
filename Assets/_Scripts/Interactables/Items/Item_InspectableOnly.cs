using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item_InspectableOnly : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private bool _rotateX = true, _rotateY = true, _rotateZ = true;

    [SerializeField] private ITriggerAction _trigger;
    [SerializeField] private bool _alreadyTriggered;
    [SerializeField] private float _triggerDelay;

    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }

    public void Interact(PlayerController playerController)
    {
        bool[] rotateXYZ = new bool[] { _rotateX, _rotateY, _rotateZ };
        playerController.PlayerInspect.Inspect(transform, rotateXYZ); 
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

    public void TriggerActions()
    {
        _trigger = gameObject.GetComponent<ITriggerAction>();

        if (_trigger != null && !_alreadyTriggered)
        {
            _trigger.TriggerAction(_triggerDelay);
            _alreadyTriggered = true;
        }
    }
}
