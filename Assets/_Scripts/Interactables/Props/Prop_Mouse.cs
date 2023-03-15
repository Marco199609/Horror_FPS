using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Mouse : MonoBehaviour, IInteractable
{
    [SerializeField] private Material _screenMaterial;
    [SerializeField] private AudioClip _mouseClick;
    [SerializeField] private float _audioVolume = 0.2f;
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
        return null;
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

        SoundManager.Instance.PlaySoundEffect(_mouseClick, transform.position, _audioVolume); ;
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
