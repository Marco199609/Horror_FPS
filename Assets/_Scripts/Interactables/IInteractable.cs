using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool NonInspectable();
    bool InspectableOnly();
    string InteractableDescription();
    string ActionDescription();
    void Interact(PlayerController playerController);
}
