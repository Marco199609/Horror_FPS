using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item_InspectableOnly : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private bool _rotateX = true, _rotateY = true, _rotateZ = true;

    [SerializeField] private ITrigger _trigger;
    [SerializeField] private bool _alreadyTriggered;
    [SerializeField] private float _triggerDelay;

    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }

    public void Interact(PlayerController playerController, bool isInteracting, bool isInspecting)
    {
        if(isInspecting)
        {
            bool[] rotateXYZ = new bool[] { _rotateX, _rotateY, _rotateZ };
            playerController.PlayerInspect.Inspect(transform, rotateXYZ);
        }
    }

    public bool[] InteractableNonInspectableOrInspectableOnly()
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
        _trigger = gameObject.GetComponent<ITrigger>();

        if (_trigger != null && !_alreadyTriggered)
        {
            _trigger.TriggerBehaviour(_triggerDelay, false, false);
            _alreadyTriggered = true;
        }
    }
}
