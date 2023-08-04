using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Prop_Drawer : MonoBehaviour, IInteractable
{
    [SerializeField] private float _moveVelocity;
    [SerializeField] private Transform _drawerOpenPosition, _drawerClosedPosition;
    [SerializeField] private GameObject _key;

    private bool _changeDrawerState;

    private IPlayerInventory _inventory;


    private enum DrawerState { Locked, Closed, Open };
    [SerializeField] private DrawerState _currentDrawerState;

    public string ActionDescription()
    {
        if (_currentDrawerState == DrawerState.Closed)
            return "Open";
        else if (_currentDrawerState == DrawerState.Locked)
            return "Locked";
        else
            return "Close";
    }

    public void Interact(PlayerController playerController)
    {
        if (_inventory == null) _inventory = playerController.Inventory;
        _changeDrawerState = true;
    }

    public string InteractableDescription()
    {
        return null;
    }



    private void Update()
    {
        Behaviour();
    }

    public void Behaviour()
    {
        if (_changeDrawerState)
        {
            //if (_doorCollider.enabled) _doorCollider.enabled = false;

            switch (_currentDrawerState)
            {
                case DrawerState.Locked:
                    {
                        if (_key != null && _inventory.SelectedItem() == _key)
                        {
                            SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.DrawerUnlockClip, transform.position, SoundManager.Instance.DrawerUnlockClipVolume, true);
                            _inventory.Remove(_key);
                            Destroy(_key);

                            _currentDrawerState = DrawerState.Closed;
                            _changeDrawerState = false;
                        }
                        else
                        {
                            SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.DrawerLockedClip, transform.position, SoundManager.Instance.DrawerLockedClipVolume, true);
                        }
                        _changeDrawerState = false;
                        break;
                    }
                case DrawerState.Closed:
                    {
                        SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.DrawerOpenClip, transform.position, SoundManager.Instance.DrawerOpenClipVolume, true);

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
                        SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.DrawerCloseClip, transform.position, SoundManager.Instance.DrawerCloseClipVolume, true);

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
}
