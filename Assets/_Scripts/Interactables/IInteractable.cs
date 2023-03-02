using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string InteractableDescription();
    string ActionDescription();
    void Interact(PlayerController playerController);
}
