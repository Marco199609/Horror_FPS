using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Prop_LivingRoomPhoto : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private bool _rotateX = true, _rotateY = true, _rotateZ = true;

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

    }
}
