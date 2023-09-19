using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Prop_Drawer : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private float _moveVelocity;
    [SerializeField] private Transform _drawerOpenPosition, _drawerClosedPosition;
    [SerializeField] private GameObject _key;
    [SerializeField] private Vector3 _inspectableInitialRotation;

    private bool _changeDrawerState;

    private IPlayerInventory _inventory;
    private SoundData _soundData;

    private enum DrawerState { Locked, Closed, Open };
    [SerializeField] private DrawerState _currentDrawerState;

    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }

    public void Interact(PlayerController playerController, bool isInteracting, bool isInspecting)
    {
        if (_inventory == null) _inventory = playerController.Inventory;
        _changeDrawerState = true;
    }

    private void Update()
    {
        Behaviour();
    }

    public void Behaviour()
    {
        if (_soundData == null) _soundData = SoundManager.Instance.SoundData;
        if (_changeDrawerState)
        {
            //if (_doorCollider.enabled) _doorCollider.enabled = false;

            switch (_currentDrawerState)
            {
                case DrawerState.Locked:
                    {
                        if (_key != null && _inventory.SelectedItem() == _key)
                        {
                            SoundManager.Instance.PlaySoundEffect(_soundData.DrawerUnlockClip, transform.position, _soundData.DrawerUnlockClipVolume, true);
                            _inventory.Remove(_key);
                            Destroy(_key);

                            _currentDrawerState = DrawerState.Closed;
                            _changeDrawerState = false;
                        }
                        else
                        {
                            SoundManager.Instance.PlaySoundEffect(_soundData.DrawerLockedClip, transform.position, _soundData.DrawerLockedClipVolume, true);
                        }
                        _changeDrawerState = false;
                        break;
                    }
                case DrawerState.Closed:
                    {
                        SoundManager.Instance.PlaySoundEffect(_soundData.DrawerOpenClip, transform.position, _soundData.DrawerOpenClipVolume, true);

                        if(transform.localPosition.x > _drawerOpenPosition.localPosition.x + 0.02f)
                            transform.localPosition = Vector3.Lerp(transform.localPosition, _drawerOpenPosition.localPosition, _moveVelocity * Time.deltaTime);
                        else
                        {
                            _currentDrawerState = DrawerState.Open;
                            _changeDrawerState = false;
                        }
                        break;
                    }
                case DrawerState.Open:
                    {
                        SoundManager.Instance.PlaySoundEffect(_soundData.DrawerCloseClip, transform.position, _soundData.DrawerCloseClipVolume, true);

                        if (transform.localPosition.x < _drawerClosedPosition.localPosition.x - 0.02f)
                            transform.localPosition = Vector3.Lerp(transform.localPosition, _drawerClosedPosition.localPosition, _moveVelocity * Time.deltaTime);
                        else
                        {
                            _currentDrawerState = DrawerState.Closed;
                            _changeDrawerState = false;
                        }
                        break;
                    }
            }
        }
        //else if (!_doorCollider.enabled) _doorCollider.enabled = true;
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
