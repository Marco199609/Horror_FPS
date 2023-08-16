using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item_InspectableOnly : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private bool _rotateX = true, _rotateY = true, _rotateZ = true;
    [SerializeField] private string _inspectableDescription;

    [SerializeField] private ITriggerAction _trigger;
    [SerializeField] private bool _alreadyTriggered;
    [SerializeField] private float _triggerDelay;

    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }

    public string ActionDescription()
    {
        return "Inspect";
    }

    public void Interact(PlayerController playerController)
    {
        bool[] rotateXYZ = new bool[] { _rotateX, _rotateY, _rotateZ };
        playerController.PlayerInspect.Inspect(transform, rotateXYZ);

        _trigger = gameObject.GetComponent<ITriggerAction>();
        if(_trigger != null && !_alreadyTriggered)
        {
            _alreadyTriggered = TriggerActions(_trigger, _alreadyTriggered, _triggerDelay);
        }
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

    public bool TriggerActions(ITriggerAction trigger, bool alreadyTriggered, float triggerDelay)
    {
        trigger.TriggerAction(triggerDelay);
        return true;
    }
}
