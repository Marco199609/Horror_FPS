using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Mouse : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private Material _screenMaterial;
    [SerializeField] private Light _screenLight;
    [SerializeField] private bool _screenOn;
    [SerializeField] private Vector3 _inspectableInitialRotation;

    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }

    private void Start()
    {
        _screenMaterial.DisableKeyword("_EMISSION");
        _screenLight.enabled = false;
        _screenOn = false;
    }

    public void Interact(PlayerController playerController, bool isInteracting, bool isInspecting)
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

        SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.SoundData.MouseClickClip, transform.position, SoundManager.Instance.SoundData.MouseClickClipVolume); ;
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
        throw new System.NotImplementedException();
    }

    public Vector3 InspectableInitialRotation()
    {
        return _inspectableInitialRotation;
    }
}
