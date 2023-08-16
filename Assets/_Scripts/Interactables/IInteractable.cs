using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void AssignInStateLoader();
    bool[] InteractableType(); //Index 0 is non inspectable, index 1 es inspectable only
    string InteractableDescription();
    string ActionDescription();
    void Interact(PlayerController playerController);
    bool[] RotateXYZ();
    bool TriggerActions(ITriggerAction trigger, bool alreadyTriggered, float triggerDelay);
}
