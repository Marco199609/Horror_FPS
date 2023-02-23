using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Prop_Door : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _doorHandle, _doorPivotPoint;
    [SerializeField] Collider _doorCollider;
    [SerializeField] AudioSource _doorLockedAudioSource;
    [SerializeField] AudioClip _doorLockedClip, _doorOpenClip, _doorCloseClip;
    [SerializeField] private GameObject _requiredKey;

    private float _doorMoveVelocity = 150;

    private enum DoorState {Locked, Closed, Open};
    [SerializeField] private DoorState _currentDoorState;
    private bool _changeDoorState;
    public string InteractableDescription()
    {
        if (_currentDoorState == DoorState.Closed)
            return "";
        else if (_currentDoorState == DoorState.Locked)
            return "Door locked";
        else
            return "";
    }
    public string ActionDescription()
    {
        if (_currentDoorState == DoorState.Closed)
            return "Open door";
        else if (_currentDoorState == DoorState.Locked)
            return "Find key";
        else
            return "Close door";
    }

    public void Interact()
    {
        _changeDoorState = true;
    }

    private void Update()
    {
        Behaviour();
    }

    public void Behaviour()
    {
        if (_changeDoorState)
        {
            if (_doorCollider.enabled) _doorCollider.enabled = false;

            switch (_currentDoorState)
            {
                case DoorState.Locked:
                    {
                        PlayerController playerController = FindObjectOfType<PlayerController>();

                        if(playerController.SelectedInventoryItem == _requiredKey)
                        {
                            playerController.Inventory.Remove(_requiredKey);
                            playerController.GetComponent<PlayerInventory>().Index = 0;

                            Destroy(_requiredKey);
                            _currentDoorState = DoorState.Closed;
                            _changeDoorState = false;
                        }
                        else
                        {
                            if (!_doorLockedAudioSource.isPlaying)
                                _doorLockedAudioSource.PlayOneShot(_doorLockedClip, 0.2f);
                        }
                        _changeDoorState = false;
                        break;
                    }
                case DoorState.Closed:
                    {
                        if (!_doorLockedAudioSource.isPlaying)
                            _doorLockedAudioSource.PlayOneShot(_doorOpenClip, 0.2f);

                        if (_doorPivotPoint.transform.localEulerAngles.z > 270 || _doorPivotPoint.transform.localEulerAngles.z == 0)
                        {
                            _doorPivotPoint.transform.Rotate(0, 0, -_doorMoveVelocity * Time.deltaTime);
                        }
                        else
                        {
                            _currentDoorState = DoorState.Open;
                            _changeDoorState = false;
                        }
                        break;
                    }
                case DoorState.Open:
                    {
                        if (!_doorLockedAudioSource.isPlaying)
                            _doorLockedAudioSource.PlayOneShot(_doorCloseClip, 0.2f);

                        if (_doorPivotPoint.transform.localEulerAngles.z > 260)
                        {
                            _doorPivotPoint.transform.Rotate(0, 0, _doorMoveVelocity * Time.deltaTime);
                        }
                        else if (_doorPivotPoint.transform.localEulerAngles.z > 0)
                            _doorPivotPoint.transform.localEulerAngles = new Vector3(_doorPivotPoint.transform.localEulerAngles.x, _doorPivotPoint.transform.localEulerAngles.y, 0);
                        else
                        {
                            _currentDoorState = DoorState.Closed;
                            _changeDoorState = false;
                        }
                        break;
                    }
            }
        }
        else if (!_doorCollider.enabled) _doorCollider.enabled = true;
    }
}
