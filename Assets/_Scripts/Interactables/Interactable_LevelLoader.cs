using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_LevelLoader : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;

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
    public string ActionDescription()
    {
        return "";
    }

    public void Interact(PlayerController playerController)
    {
        gameObject.GetComponent<Trigger_LevelLoader>().LoadLevel();
    }

    public string InteractableDescription()
    {
        return "";
    }

    public bool[] InteractableType()
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

    public bool TriggerActions(ITriggerAction trigger, bool alreadyTriggered, float triggerDelay)
    {
        throw new System.NotImplementedException();
    }
}
