using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_LevelLoader : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private Vector3 _inspectableInitialRotation;

    private void OnEnable()
    {
        AssignInStateLoader();
    }
    private void OnDestroy()
    {
        if (_id != 0) SceneStateLoader.Instance.objects.Remove(_id);
    }
    public void AssignInStateLoader()
    {
        if (_id != 0) SceneStateLoader.Instance.objects.Add(_id, gameObject);
        else print("id is 0 in gameobject " + gameObject.name + "!");
    }

    public void Interact(PlayerController playerController, bool isInteracting, bool isInspecting)
    {
        gameObject.GetComponent<Trigger_LevelLoader>().TriggerBehaviour(0, false, false);
    }

    public bool[] InteractableIsNonInspectableOrInspectableOnly()
    {
        bool nonInspectable = true;
        bool inspectableOnly = false;

        bool[] interactableType = new bool[] { nonInspectable, inspectableOnly };

        return interactableType;
    }

    public bool[] RotateXYZ()
    {
        bool[] rotateXYZ = new bool[] { false, false, false };

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
