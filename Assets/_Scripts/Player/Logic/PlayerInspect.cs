using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInspect : MonoBehaviour, IPlayerInspect
{
    private float _goToInspectionPositionSpeed = 15f, _deleteCurrentInspectableTimer = 0.7f, _timer;
    private bool _inspectingItem;

    private Transform _currentInspectableSelected;
    private Vector3 _currentItemRotation;

    private Transform _previousParent;
    private Vector3 _previousPosition;
    private Quaternion _previousRotation;

    public void Inspect(Transform inspectable)
    {
        _inspectingItem = true;
        _currentInspectableSelected = inspectable;
        _previousPosition = inspectable.position;
        _previousRotation = inspectable.rotation;
        _previousParent = inspectable.parent;
        inspectable.GetComponent<Collider>().enabled = false;
        _timer = _deleteCurrentInspectableTimer;
    }

    public void ManageInspection(PlayerData playerData, IPlayerInput playerInput)
    {
        if (_currentInspectableSelected != null)
        {
            if (Input.GetMouseButtonDown(1)) _inspectingItem = false;

            if (_inspectingItem)
            {
                _currentInspectableSelected.transform.SetParent(playerData.Camera);
                _currentInspectableSelected.localPosition = Vector3.Lerp(_currentInspectableSelected.localPosition, new Vector3(0, 0, 0.5f), _goToInspectionPositionSpeed * Time.deltaTime);

                _currentItemRotation.x = 0;
                _currentItemRotation.y += playerInput.mouseMovementInput.x * 3.5f;
                _currentItemRotation.z += playerInput.mouseMovementInput.y * 3.5f;

                _currentInspectableSelected.rotation = Quaternion.Euler(_currentItemRotation);
            }
            else
            {
                _currentInspectableSelected.transform.SetParent(_previousParent);
                _currentInspectableSelected.transform.position = Vector3.Lerp(_currentInspectableSelected.position, _previousPosition, _goToInspectionPositionSpeed * Time.deltaTime);
                _currentInspectableSelected.transform.rotation = Quaternion.Lerp(_currentInspectableSelected.rotation, _previousRotation, _goToInspectionPositionSpeed * Time.deltaTime);

                if (_timer > 0 && _currentInspectableSelected != null) _timer -= Time.deltaTime;
                else
                {
                    _currentInspectableSelected.GetComponent<Collider>().enabled = true;
                    _currentInspectableSelected = null;
                    _timer = _deleteCurrentInspectableTimer;
                }
            }
        }
    }

    public bool Inspecting()
    {
        return _inspectingItem;
    }
}
