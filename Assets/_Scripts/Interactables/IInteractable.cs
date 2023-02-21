using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string Description(); //Returns description to other scripts
    void Interact(); //Interactable behaviour when clicked or picked up
    void Behaviour(); //Interactable behaviour when used

}
