using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private string _actionDescription;
    [SerializeField] private List<GameObject> _lightOnObjects, _lightOffObjects; //Make sure each light on object has its light off object
    [SerializeField] private GameObject _switchOnModel, _switchOffModel;
    [SerializeField] private AudioClip _switchOnClip, _switchOffClip;
    [SerializeField] private float _clipVolume = 0.2f;

    private bool _lightsOn;
    public string ActionDescription()
    {
        return null;
    }

    public void Interact(PlayerController playerController)
    {
        if (_lightsOn)
        {
            for (int i = 0; i < _lightOnObjects.Count; i++)
            {
                _lightOnObjects[i].gameObject.SetActive(false);
                _lightOffObjects[i].gameObject.SetActive(true);
            }

            _switchOnModel.SetActive(false);
            _switchOffModel.SetActive(true);

            SoundManager.Instance.PlaySoundEffect(_switchOffClip, transform.position, _clipVolume);
            _lightsOn = false;
        }
        else
        {
            for (int i = 0; i < _lightOnObjects.Count; i++)
            {
                _lightOnObjects[i].gameObject.SetActive(true);
                _lightOffObjects[i].gameObject.SetActive(false);
            }

            _switchOnModel.SetActive(true);
            _switchOffModel.SetActive(false);

            SoundManager.Instance.PlaySoundEffect(_switchOnClip, transform.position, _clipVolume);
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

    public bool InspectableOnly()
    {
        return false;
    }
}
