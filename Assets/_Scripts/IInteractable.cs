using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(); //Interactable behaviour 
    string Description(); //Returns description to other scripts
}
