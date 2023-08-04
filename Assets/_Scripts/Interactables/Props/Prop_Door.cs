using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Prop_Door : MonoBehaviour, IInteractable
{
    //[SerializeField] GameObject _doorHandle;
    [SerializeField] Transform _doorPivotPoint;
    [SerializeField] Collider _doorCollider;
    [SerializeField] private GameObject _key;
    [SerializeField] private GameObject[] _triggers;
    [SerializeField] private bool[] _alreadyTriggered;

    private IPlayerInventory _inventory;
    private float _doorMoveVelocity;

    private enum DoorState {Locked, Closed, Open};
    [SerializeField] private DoorState _currentDoorState;
    private bool _changeDoorState;

    private void Start()
    {
        if (_key != null) _currentDoorState = DoorState.Locked;
    }

    public string InteractableDescription()
    {
        return "";
    }
    public string ActionDescription()
    {
        return "";
    }

    public void Interact(PlayerController playerController)
    {
        if(_inventory == null) _inventory = playerController.Inventory;
        _changeDoorState = true;

        if (_triggers != null)
        {
            for(int i = 0; i < _triggers.Length; i++)
            {
                TriggerActions(_triggers[i].GetComponent<ITriggerAction>(), _alreadyTriggered[i]);
            }
        }
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
                            SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.DoorUnlockClip, transform.position, SoundManager.Instance.DoorUnlockClipVolume, true);
                            _inventory.Remove(_key);
                            Destroy(_key);

                            _currentDoorState = DoorState.Closed;
                            _changeDoorState = false;
                        }
                        else
                        {
                            SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.DoorLockedClip, transform.position, SoundManager.Instance.DoorLockedClipVolume, true);
                        }
                        _changeDoorState = false;
                        break;
                    }
                case DoorState.Closed:
                    {
                        SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.DoorOpenClip, transform.position, SoundManager.Instance.DoorOpenClipVolume, true);

                        if (_doorPivotPoint.localEulerAngles.y > 270 || _doorPivotPoint.localEulerAngles.y == 0)
                        {
                            float degreesToRotate = 90;
                            float timeToRotate = SoundManager.Instance.DoorOpenClip.length;
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
                        float timeToRotate = SoundManager.Instance.DoorCloseClip.length;
                        _doorMoveVelocity = degreesToRotate / timeToRotate;

                        SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.DoorCloseClip, transform.position, SoundManager.Instance.DoorCloseClipVolume, true);

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

    public void TriggerActions(ITriggerAction trigger, bool alreadyTriggered)
    {
        if(!alreadyTriggered)
        {
            trigger.TriggerAction();
            alreadyTriggered = true;
        }
    }
}
