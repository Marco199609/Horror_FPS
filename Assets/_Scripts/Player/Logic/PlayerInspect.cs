using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInspect : MonoBehaviour
{
    private float _goToInspectionPositionSpeed = 0.1f, _deleteCurrentInspectableTimer = 0.5f;
    private bool _inspectingItem;

    private Transform _currentInspectableSelected;
    private Vector3 _currentItemRotation;
    private Vector3 _previousPosition;
    private Quaternion _previousRotation;

    public void Inspect(Transform inspectable)
    {
        _inspectingItem = true;
        _currentInspectableSelected = inspectable;
        _previousPosition = inspectable.position;
        _previousRotation = inspectable.rotation;
    }

    public void ManageInspection(PlayerData playerData, IPlayerInput playerInput)
    {
        if (_currentInspectableSelected != null)
        {
            if (Input.GetMouseButtonDown(1)) _inspectingItem = false;

            if (_inspectingItem)
            {
                _currentInspectableSelected.transform.SetParent(playerData.Camera);
                _currentInspectableSelected.localPosition = Vector3.Lerp(_currentInspectableSelected.localPosition, new Vector3(0, 0, 0.5f), _goToInspectionPositionSpeed);

                _currentItemRotation.x = 0;
                _currentItemRotation.y += playerInput.mouseMovementInput.x * 3.5f;
                _currentItemRotation.z += playerInput.mouseMovementInput.y * 3.5f;

                _currentInspectableSelected.rotation = Quaternion.Euler(_currentItemRotation);
            }
            else
            {
                _currentInspectableSelected.transform.SetParent(null);
                _currentInspectableSelected.transform.position = Vector3.Lerp(_currentInspectableSelected.position, _previousPosition, _goToInspectionPositionSpeed);
                _currentInspectableSelected.transform.rotation = Quaternion.Lerp(_currentInspectableSelected.rotation, _previousRotation, _goToInspectionPositionSpeed);

                if (_deleteCurrentInspectableTimer > 0 && _currentInspectableSelected != null) _deleteCurrentInspectableTimer -= Time.deltaTime;
                else
                {
                    _currentInspectableSelected = null;
                    _deleteCurrentInspectableTimer = 0.5f;
                }
            }
        }
    }

    public bool Inspecting()
    {
        return _inspectingItem;
    }
}
