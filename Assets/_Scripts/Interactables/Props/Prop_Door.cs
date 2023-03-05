using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Prop_Door : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _nonInspectable = true;
    [SerializeField] GameObject _doorHandle;
    [SerializeField] Transform _doorPivotPoint;
    [SerializeField] Collider _doorCollider;
    [SerializeField] AudioSource _doorLockedAudioSource;
    [SerializeField] AudioClip _doorLockedClip, _doorOpenClip, _doorCloseClip;
    [SerializeField] private GameObject _key;

    private IPlayerInventory _inventory;
    private float _doorMoveVelocity;

    private enum DoorState {Locked, Closed, Open};
    [SerializeField] private DoorState _currentDoorState;
    private bool _changeDoorState;


    private void Awake()
    {
        
    }

    public string InteractableDescription()
    {
        if (_currentDoorState == DoorState.Closed)
            return "";
        else if (_currentDoorState == DoorState.Locked)
            return "";
        else
            return "";
    }
    public string ActionDescription()
    {
        if (_currentDoorState == DoorState.Closed)
            return "Open";
        else if (_currentDoorState == DoorState.Locked)
        {
            return "Locked";
        }
        else
            return "Close";
    }

    public void Interact(PlayerController playerController)
    {
        if(_inventory == null) _inventory = playerController.Inventory;
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
                        if (_key != null && _inventory.SelectedItem() == _key)
                        {
                            _inventory.Remove(_key);
                            Destroy(_key);

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
                        if(_changeDoorState) //Prevents the door from opening automatically when key inserted
                        {
                            if (!_doorLockedAudioSource.isPlaying)
                                _doorLockedAudioSource.PlayOneShot(_doorOpenClip, 0.2f);

                            if (_doorPivotPoint.localEulerAngles.y > 270 || _doorPivotPoint.localEulerAngles.y == 0)
                            {
                                float degreesToRotate = 90;
                                float timeToRotate = _doorOpenClip.length;
                                _doorMoveVelocity = degreesToRotate / timeToRotate;

                                _doorPivotPoint.Rotate(0, -_doorMoveVelocity * Time.deltaTime, 0);
                            }
                            else
                            {
                                _currentDoorState = DoorState.Open;
                                _changeDoorState = false;
                            }
                        }
                        break;
                    }
                case DoorState.Open:
                    {
                        if(_changeDoorState)
                        {
                            float degreesToRotate = 90;
                            float timeToRotate = _doorCloseClip.length;
                            _doorMoveVelocity = degreesToRotate / timeToRotate;

                            if (!_doorLockedAudioSource.isPlaying)
                                _doorLockedAudioSource.PlayOneShot(_doorCloseClip, 0.2f);

                            if (_doorPivotPoint.localEulerAngles.y > 260)
                            {
                                _doorPivotPoint.Rotate(0, _doorMoveVelocity * Time.deltaTime, 0);
                            }
                            else if (_doorPivotPoint.localEulerAngles.y > 0)
                                _doorPivotPoint.localEulerAngles = new Vector3(_doorPivotPoint.localEulerAngles.x, 0, _doorPivotPoint.localEulerAngles.z);
                            else
                            {
                                _currentDoorState = DoorState.Closed;
                                _changeDoorState = false;
                            }
                        }
                        break;
                    }
            }
        }
        else if (!_doorCollider.enabled) _doorCollider.enabled = true;
    }

    public bool NonInspectable()
    {
        return _nonInspectable;
    }
}
