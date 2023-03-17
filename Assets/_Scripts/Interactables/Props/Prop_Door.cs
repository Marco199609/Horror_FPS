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
    [SerializeField] AudioClip _doorLockedClip, _doorUnlockedClip, _doorOpenClip, _doorCloseClip;
    [SerializeField] private float _clipVolume = 0.2f;
    [SerializeField] private GameObject _key;

    private IPlayerInventory _inventory;
    private float _doorMoveVelocity;

    private enum DoorState {Locked, Closed, Open};
    [SerializeField] private DoorState _currentDoorState;
    private bool _changeDoorState;

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
                            SoundManager.Instance.PlaySoundEffect(_doorUnlockedClip, transform.position, _clipVolume, true);
                            _inventory.Remove(_key);
                            Destroy(_key);

                            _currentDoorState = DoorState.Closed;
                            _changeDoorState = false;
                        }
                        else
                        {
                            SoundManager.Instance.PlaySoundEffect(_doorLockedClip, transform.position, _clipVolume, true);
                        }
                        _changeDoorState = false;
                        break;
                    }
                case DoorState.Closed:
                    {
                        SoundManager.Instance.PlaySoundEffect(_doorOpenClip, transform.position, _clipVolume, true);

                        if (_doorPivotPoint.localEulerAngles.y > 270 || _doorPivotPoint.localEulerAngles.y == 0)
                        {
                            float degreesToRotate = 90;
                            float timeToRotate = _doorOpenClip.length;
                            _doorMoveVelocity = degreesToRotate / timeToRotate;

                            _doorPivotPoint.Rotate(0, -_doorMoveVelocity * Time.deltaTime * 1.05f, 0); //1.05 prevents audioclip from playing mor than once
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
                        float degreesToRotate = 90;
                        float timeToRotate = _doorCloseClip.length;
                        _doorMoveVelocity = degreesToRotate / timeToRotate;

                        SoundManager.Instance.PlaySoundEffect(_doorCloseClip, transform.position, _clipVolume, true);

                        if (_doorPivotPoint.localEulerAngles.y > 260)
                        {
                            _doorPivotPoint.Rotate(0, _doorMoveVelocity * Time.deltaTime * 1.05f, 0); //1.05 prevents audioclip from playing mor than once
                        }
                        else if (_doorPivotPoint.localEulerAngles.y > 0)
                            _doorPivotPoint.localEulerAngles = new Vector3(_doorPivotPoint.localEulerAngles.x, 0, _doorPivotPoint.localEulerAngles.z);
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

    public bool NonInspectable()
    {
        return _nonInspectable;
    }

    public bool InspectableOnly()
    {
        return false;
    }
}
