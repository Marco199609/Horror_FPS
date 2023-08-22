using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInspect
{
    void Inspect(Transform inspectable, bool[] rotateXYZ);
    bool Inspecting();
    void ManageInspection(PlayerData playerData, IPlayerInput playerInput);
}

public class PlayerInspect : MonoBehaviour, IPlayerInspect
{
    private float _goToInspectionPositionSpeed = 30f, _deleteCurrentInspectableTimer = 0.7f, _timer, _rotationSpeed = 500f;
    private bool _inspectingItem;

    private Transform _currentInspectableSelected;
    private Vector3 _currentItemRotation;
    private bool _rotateX, _rotateY, _rotateZ;

    private Transform _previousParent;
    private Vector3 _previousPosition, _previousScale;
    private Quaternion _previousRotation;

    private PlayerData _playerData;
    public void Inspect(Transform inspectable, bool[] rotateXYZ)
    {
        _currentInspectableSelected = inspectable;
        _previousPosition = inspectable.position;
        _previousScale = inspectable.localScale;
        _previousRotation = inspectable.rotation;
        _previousParent = inspectable.parent;
        _rotateX = rotateXYZ[0];
        _rotateY = rotateXYZ[1];
        _rotateZ = rotateXYZ[2];

        inspectable.GetComponent<Collider>().enabled = false;
        _timer = _deleteCurrentInspectableTimer;
        SoundManager.Instance.Play2DSoundEffect(SoundManager.Instance.SoundData.ItemInspectClip, SoundManager.Instance.SoundData.ItemInspectClipVolume);

        _inspectingItem = true;
    }

    public void ManageInspection(PlayerData playerData, IPlayerInput playerInput)
    {
        if (_playerData == null) _playerData = playerData;

        if (_currentInspectableSelected != null)
        {
            if (Input.GetMouseButtonDown(1)) _inspectingItem = false;

            if (_inspectingItem)
            {
                _currentInspectableSelected.transform.SetParent(playerData.Camera);
                _currentInspectableSelected.localPosition = Vector3.Lerp(_currentInspectableSelected.localPosition, new Vector3(0, 0, 1f), _goToInspectionPositionSpeed * Time.deltaTime);

                if (Input.GetMouseButton(0) && _rotateX) _currentItemRotation.x += playerInput.mouseMovementInput.y * _rotationSpeed* Time.deltaTime; //Changes rotation axis if necessary
                else if (_rotateZ) _currentItemRotation.z += playerInput.mouseMovementInput.y * _rotationSpeed * Time.deltaTime;
                if (_rotateY) _currentItemRotation.y += playerInput.mouseMovementInput.x * _rotationSpeed * Time.deltaTime;

                _currentInspectableSelected.localRotation = Quaternion.Euler(_currentItemRotation);
            }
            else
            {
                if (_currentInspectableSelected.transform.parent != _previousParent) 
                    SoundManager.Instance.Play2DSoundEffect(
                        SoundManager.Instance.SoundData.ItemInspectClip, 
                        SoundManager.Instance.SoundData.ItemInspectClipVolume);

                _currentInspectableSelected.transform.SetParent(_previousParent);
                _currentInspectableSelected.position = Vector3.Lerp(_currentInspectableSelected.position, _previousPosition, _goToInspectionPositionSpeed * Time.deltaTime);
                _currentInspectableSelected.localScale = Vector3.Lerp(_currentInspectableSelected.localScale, _previousScale, _goToInspectionPositionSpeed * Time.deltaTime);
                _currentInspectableSelected.rotation = Quaternion.Lerp(_currentInspectableSelected.rotation, _previousRotation, _goToInspectionPositionSpeed * Time.deltaTime);

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
