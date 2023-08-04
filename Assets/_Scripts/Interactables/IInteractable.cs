using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool[] InteractableType(); //Index 0 is non inspectable, index 1 es inspectable only
    string InteractableDescription();
    string ActionDescription();
    void Interact(PlayerController playerController);
    bool[] RotateXYZ();
    void TriggerActions(ITriggerAction trigger, bool alreadyTriggered);
}
