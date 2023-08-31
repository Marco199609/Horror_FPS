using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum DoorState { Locked, Closed, Open };

public class Prop_Door : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] Transform _doorPivotPoint;
    [SerializeField] Collider _doorCollider;
    [SerializeField] private GameObject _key;
    [SerializeField] private GameObject[] _triggers;
    [SerializeField] private float[] _triggerDelays;
    [SerializeField] private bool[] _alreadyTriggered;
    [SerializeField] private Material _emmisiveMaterial, _nonEmissiveMaterial;

    public void AssignInStateLoader()
    {
        if (_id != 0) SceneStateLoader.Instance.objects.Add(_id, gameObject);
        else print("id is 0 in gameobject " + gameObject.name + "!");
    }

    private IPlayerInventory _inventory;
    private float _doorMoveVelocity;
    private SoundData _soundData;

    public DoorState _currentDoorState;
    private bool _changeDoorState;

    private void OnEnable()
    {
        AssignInStateLoader();
    }



    private void OnDestroy()
    {
        if (_id != 0) SceneStateLoader.Instance.objects.Remove(_id);
    }

    public void Interact(PlayerController playerController)
    {
        if(_inventory == null) _inventory = playerController.Inventory;
        if (_soundData == null) _soundData = SoundManager.Instance.SoundData;
        _changeDoorState = true;
    }

    private void Update()
    {
        Behaviour();

        if (_key == null && _currentDoorState == DoorState.Locked)
        {
            GetComponent<Renderer>().material = _nonEmissiveMaterial;
        }
        else if (_currentDoorState == DoorState.Closed)
        {
            GetComponent<Renderer>().material = _emmisiveMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = _emmisiveMaterial;
        }
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
                            SoundManager.Instance.PlaySoundEffect(_soundData.DoorUnlockClip, transform.position, _soundData.DoorUnlockClipVolume, true);
                            _inventory.Remove(_key);
                            Destroy(_key);

                            _currentDoorState = DoorState.Closed;
                            _changeDoorState = false;
                        }
                        else
                        {
                            SoundManager.Instance.PlaySoundEffect(_soundData.DoorLockedClip, transform.position, _soundData.DoorLockedClipVolume, true);
                        }
                        _changeDoorState = false;
                        break;
                    }
                case DoorState.Closed:
                    {
                        SoundManager.Instance.PlaySoundEffect(_soundData.DoorOpenClip, transform.position, _soundData.DoorOpenClipVolume, true);

                        if (_doorPivotPoint.localEulerAngles.y > 270 || _doorPivotPoint.localEulerAngles.y == 0)
                        {
                            float degreesToRotate = 90;
                            float timeToRotate = _soundData.DoorOpenClip.length;
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
                        float timeToRotate = _soundData.DoorCloseClip.length;
                        _doorMoveVelocity = degreesToRotate / timeToRotate;

                        SoundManager.Instance.PlaySoundEffect(_soundData.DoorCloseClip, transform.position, _soundData.DoorCloseClipVolume, true);

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

    public void TriggerActions()
    {
        if (_triggers != null)
        {
            for (int i = 0; i < _triggers.Length; i++)
            {
                if (!_alreadyTriggered[i])
                {
                    _triggers[i].GetComponent<ITriggerAction>().TriggerAction(_triggerDelays[i]);
                    _alreadyTriggered[i] = true;
                }
            }
        }
    }
}
