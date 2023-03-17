using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Prop_Drawer : MonoBehaviour, IInteractable
{
    [SerializeField] private float _moveVelocity;
    [SerializeField] private Transform _drawerOpenPosition, _drawerClosedPosition;
    [SerializeField] private GameObject _key;
    [SerializeField] private AudioClip _drawerOpenClip, _drawerCloseClip, _drawerLockedClip;
    [SerializeField] private float _audioVolume = 0.2f;

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

    public bool NonInspectable()
    {
        return true;
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
                            _inventory.Remove(_key);
                            Destroy(_key);

                            _currentDrawerState = DrawerState.Closed;
                            _changeDrawerState = false;
                        }
                        else
                        {
                            SoundManager.Instance.PlaySoundEffect(_drawerLockedClip, transform.position, _audioVolume, true);
                        }
                        _changeDrawerState = false;
                        break;
                    }
                case DrawerState.Closed:
                    {
                        SoundManager.Instance.PlaySoundEffect(_drawerOpenClip, transform.position, _audioVolume, true);

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
                        SoundManager.Instance.PlaySoundEffect(_drawerCloseClip, transform.position, _audioVolume, true);

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

    public bool InspectableOnly()
    {
        return false;
    }
}
