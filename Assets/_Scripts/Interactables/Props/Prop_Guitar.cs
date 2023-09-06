using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Guitar : MonoBehaviour, IInteractable
{
    private int _currentStrumIndex = 0;
    private AudioSource _guitarStrumSource;
    private SoundManager _soundManager;

    private void SetVariables()
    {
        if (_soundManager == null) _soundManager = SoundManager.Instance;
        if (_guitarStrumSource == null) _guitarStrumSource = _soundManager.CreateModifiableAudioSource(null, gameObject, _soundManager.SoundData.GuitarStrumClipVolume);
        _guitarStrumSource.spatialBlend = 1;
    }
    public void AssignInStateLoader()
    {
        
    }

    public void Interact(PlayerController playerController, bool isInteracting, bool isInspecting)
    {
        SetVariables();

        if(_currentStrumIndex == 0 || _currentStrumIndex == 1)  //Place strum clips in grades I, VI, IV, and V
        {
            _currentStrumIndex = Random.Range(2, 4); //minInclusive, maxExclusive
        }
        else
        {
            _currentStrumIndex = Random.Range(0, 2); //minInclusive, maxExclusive
        }

        _guitarStrumSource.clip = _soundManager.SoundData.GuitarStrumClips[_currentStrumIndex];
        _guitarStrumSource.Play();
    }

    public bool[] InteractableNonInspectableOrInspectableOnly()
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
}
