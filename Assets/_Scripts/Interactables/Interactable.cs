using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Interactable : MonoBehaviour, IInteractable
{
    [Header("Level Loader ID")]
    [SerializeField] private int _id;
    [Header("Interactable Type")]
    [SerializeField] private bool _nonInspectable;
    [SerializeField] private bool _inspectableOnly;

    [Header("Inspectable Rotation (Leave blank if non inspectable)")]
    [SerializeField] private Vector3 _inspectableInitialRotation;
    [SerializeField] private bool _rotateX;
    [SerializeField] private bool _rotateY;
    [SerializeField] private bool _rotateZ;

    [Header("Freeze Player (If behaviour requieres it)")]
    [SerializeField] private bool _freezePlayerRotation;
    [SerializeField] private bool _freezePlayerMovement;

    [Header("Behaviour Components")]
    [SerializeField] private GameObject[] _interactionBehaviours;
    [SerializeField] private GameObject[] _inspectionBehaviours;

    [Header("Deactivate behaviour once done")]
    [SerializeField] private bool _deactivateBehaviour;
    public void AssignInStateLoader()
    {
        if (_id != 0) SceneStateLoader.Instance.objects.Add(_id, gameObject);
        else print("id is 0 in gameobject " + gameObject.name + "!");
    }

    public void Interact(PlayerController playerController, bool isInteracting, bool isInspecting)
    {
        if (isInteracting)
        {
            for(int i = 0; i < _interactionBehaviours.Length; i++)
            {
                _interactionBehaviours[i].GetComponent<ITrigger>().TriggerBehaviour(0);
            }
        }
        else if(!_nonInspectable && isInspecting)
        {
            for (int i = 0; i < _inspectionBehaviours.Length; i++)
            {
                _inspectionBehaviours[i].GetComponent<ITrigger>().TriggerBehaviour(0);
            }
        }

        //Remember to unfreeze player in behaviour components
        if (_freezePlayerMovement) playerController.FreezePlayerMovement = true;
        if(_freezePlayerRotation) playerController.FreezePlayerRotation = true;
    }

    public bool[] InteractableNonInspectableOrInspectableOnly()
    {
        bool[] interactableType = new bool[] { _nonInspectable, _inspectableOnly };
        return interactableType;
    }

    public bool[] RotateXYZ()
    {
        bool[] rotateXYZ = new bool[] { _rotateX, _rotateY, _rotateZ };
        return rotateXYZ;
    }
}
