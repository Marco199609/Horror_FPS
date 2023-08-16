using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private string _actionDescription;
    [SerializeField] private List<GameObject> _lightOnObjects, _lightOffObjects; //Make sure each light on object has its light off object
    [SerializeField] private GameObject _switchOnModel, _switchOffModel;

    private bool _lightsOn;

    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }
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

            SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.LightSwitchOffClip, transform.position, SoundManager.Instance.LightSwitchOffClipVolume);
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

            SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.LightSwitchOnClip, transform.position, SoundManager.Instance.LightSwitchOnClipVolume);
            _lightsOn = true;
        }
    }

    public string InteractableDescription()
    {
        return null;
    }
    public bool[] InteractableType()
    {
        bool nonInspectable = true;
        bool inspectableOnly = false;

        bool[] interactableType = new bool[] { nonInspectable, inspectableOnly };

        return interactableType;
    }

    public bool[] RotateXYZ()
    {
        bool[] rotateXYZ = new bool[] { false, false, false };

        return rotateXYZ;
    }

    public bool TriggerActions(ITriggerAction trigger, bool alreadyTriggered, float triggerDelay)
    {
        throw new System.NotImplementedException();
    }
}
