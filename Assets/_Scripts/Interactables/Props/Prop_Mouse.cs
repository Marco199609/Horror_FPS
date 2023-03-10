using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Mouse : MonoBehaviour, IInteractable
{
    [SerializeField] private Material _screenMaterial;
    [SerializeField] private AudioSource _mouseClick;
    [SerializeField] private Light _screenLight;
    [SerializeField] private bool _screenOn;



    private void Start()
    {
        if (_screenOn)
        {
            _screenMaterial.EnableKeyword("_EMISSION");
            _screenLight.enabled = true;
        }
        else
        {
            _screenMaterial.DisableKeyword("_EMISSION");
            _screenLight.enabled = false;
        } 
    }

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
            _screenLight.enabled = false;
            _screenOn = false;
        }
        else
        {
            _screenMaterial.EnableKeyword("_EMISSION");
            _screenLight.enabled = true;
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

    public bool InspectableOnly()
    {
        return false;
    }
}
