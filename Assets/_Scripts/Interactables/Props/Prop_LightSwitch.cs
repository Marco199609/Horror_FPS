using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private string _actionDescription;
    [SerializeField] private List<GameObject> _lightOnObjects, _lightOffObjects; //Make sure each light on object has its light off object

    private bool _lightsOn;
    public string ActionDescription()
    {
        if (_lightsOn)
        {
            return "Turn lights off";
        }
        else
        {
            return "Turn lights on";
        }
    }

    public void Interact(PlayerController playerController)
    {
        if(_lightsOn)
        {
            for(int  i = 0; i < _lightOnObjects.Count; i++)
            {
                _lightOnObjects[i].gameObject.SetActive(false);
                _lightOffObjects[i].gameObject.SetActive(true);
            }

            _lightsOn = false;
        }
        else
        {
            for (int i = 0; i < _lightOnObjects.Count; i++)
            {
                _lightOnObjects[i].gameObject.SetActive(true);
                _lightOffObjects[i].gameObject.SetActive(false);
            }
            _lightsOn = true;
        }
    }

    public string InteractableDescription()
    {
        return null;
    }

    public bool NonInspectable()
    {
        return true;
    }
}
