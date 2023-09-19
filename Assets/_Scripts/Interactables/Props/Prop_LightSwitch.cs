using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private List<GameObject> _lightOnObjects, _lightOffObjects; //Make sure each light on object has its light off object
    [SerializeField] private GameObject _switchOnModel, _switchOffModel;
    [SerializeField] private Vector3 _inspectableInitialRotation;

    private bool _lightsOn;
    private SoundData _soundData;

    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }

    public void Interact(PlayerController playerController, bool isInteracting, bool isInspecting)
    {
        if (_soundData == null) _soundData = SoundManager.Instance.SoundData;

        if (_lightsOn)
        {
            for (int i = 0; i < _lightOnObjects.Count; i++)
            {
                _lightOnObjects[i].gameObject.SetActive(false);
                _lightOffObjects[i].gameObject.SetActive(true);
            }

            _switchOnModel.SetActive(false);
            _switchOffModel.SetActive(true);

            SoundManager.Instance.PlaySoundEffect(_soundData.LightSwitchOffClip, transform.position, _soundData.LightSwitchOffClipVolume);
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

            SoundManager.Instance.PlaySoundEffect(_soundData.LightSwitchOnClip, transform.position, _soundData.LightSwitchOnClipVolume);
            _lightsOn = true;
        }
    }

    public bool[] InteractableIsNonInspectableOrInspectableOnly()
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

    public void TriggerActions()
    {
        //throw new System.NotImplementedException();
    }

    public Vector3 InspectableInitialRotation()
    {
        return _inspectableInitialRotation;
    }
}
