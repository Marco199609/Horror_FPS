using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Door : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _doorHandle, _doorPivotPoint;
    [SerializeField] Collider _doorCollider;
    private float _doorMoveVelocity = 150;
    private enum DoorState {Closed, Open};

    [SerializeField] private DoorState _currentDoorState;

    private bool _changeDoorState;
    public string Description()
    {
        if(_currentDoorState == DoorState.Closed)
            return "Open door";
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
                case DoorState.Closed:
                    {
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
