using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void AssignInStateLoader();
    bool[] InteractableIsNonInspectableOrInspectableOnly(); //Index 0 is non inspectable, index 1 es inspectable only
    void Interact(PlayerController playerController, bool isInteracting, bool isInspecting);
    bool[] RotateXYZ();

    Vector3 InspectableInitialRotation();
}
