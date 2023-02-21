using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string InteractableDescription(); //Returns description to other scripts
    string ActionDescription();
    void Interact(); //Interactable behaviour when clicked or picked up
    void Behaviour(); //Interactable behaviour when used

}
