using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_crawler : MonoBehaviour, IInteractable
{
    [SerializeField] private LevelManager _levelManager;
    public void Interact(PlayerController playerController)
    {
        throw new System.NotImplementedException();
    }




    public string ActionDescription()
    {
        return "";
    }

    public string InteractableDescription()
    {
        return "";
    }

    public bool InspectableOnly()
    {
        return false;
    }

    public bool NonInspectable()
    {
        return true;
    }

    public bool PassRotateX()
    {
        return false;
    }

    public bool PassRotateY()
    {
        return false;
    }

    public bool PassRotateZ()
    {
        return false;
    }
}
