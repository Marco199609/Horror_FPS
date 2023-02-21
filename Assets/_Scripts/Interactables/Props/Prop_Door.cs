using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _description;
    [SerializeField] GameObject _doorHandle, _doorPivotPoint;
    [SerializeField] Collider _doorCollider;
    private float _doorMoveVelocity = 100;
    private enum DoorState {Closed, Open};

    [SerializeField] private DoorState _currentDoorState;

    private bool _changeDoorState;
    public string Description()
    {
        return _description;
    }

    public void Interact()
    {
        Behaviour();
    }

    public void Behaviour()
    {
        _changeDoorState = true;
    }

    private void Update()
    {
        if (_changeDoorState)
        {
            if(_doorCollider.enabled) _doorCollider.enabled = false;

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
