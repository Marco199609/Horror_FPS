using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Mouse : MonoBehaviour, IInteractable
{
    [SerializeField] private Material _screenMaterial;
    [SerializeField] private bool _screenOn;
    [SerializeField] private AudioSource _mouseClick;

    public string ActionDescription()
    {
        if (_screenOn) return "Turn computer off";
        else return "Turn computer on";
    }

    public void Interact(PlayerController playerController)
    {
        if (_screenOn)
        {
            _screenMaterial.DisableKeyword("_EMISSION");
            _screenOn = false;
        }
        else
        {
            _screenMaterial.EnableKeyword("_EMISSION");
            _screenOn = true;
        }

        _mouseClick.Play();
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
