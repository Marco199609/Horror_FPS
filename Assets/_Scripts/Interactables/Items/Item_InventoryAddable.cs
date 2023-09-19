using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item_InventoryAddable : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private bool _rotateX = true, _rotateY = true, _rotateZ = true;
    [SerializeField] private bool _nonInspectable;
    [SerializeField] private Vector3 _positionInInventory, _rotationInInventory, _scaleInInventory;
    [SerializeField] private Vector3 _inspectableInitialRotation;
    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }

    public void Interact(PlayerController playerController, bool isInteracting, bool isInspecting)
    {
        playerController.Inventory.Add(gameObject, _positionInInventory, _rotationInInventory, _scaleInInventory);
    }

    public bool[] InteractableIsNonInspectableOrInspectableOnly()
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

    public void TriggerActions()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 InspectableInitialRotation()
    {
        return _inspectableInitialRotation;
    }
}
