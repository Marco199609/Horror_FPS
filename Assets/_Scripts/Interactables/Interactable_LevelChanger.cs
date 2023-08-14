using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_LevelChanger : MonoBehaviour, IInteractable
{
    public string ActionDescription()
    {
        return "";
    }

    public void Interact(PlayerController playerController)
    {
        gameObject.GetComponent<Trigger_LevelChange>().LoadLevel(playerController);
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
