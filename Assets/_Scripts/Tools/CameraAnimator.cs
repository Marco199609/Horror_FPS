using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    [SerializeField] private bool _backToStart;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _delay, _restartTime;

    [Header("If linear movement desired")]
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private float _moveSpeed;

    [Header("Rotation type (Select one)")]
    [SerializeField] private bool _lookAt;
    [SerializeField] private bool _constantRotation;
    [SerializeField] private bool _finiteRotation;

    [Header("Rotation parameters (excluding Look At)")]
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Vector3 _rotateVector;

    [Header("Look At parameters")]
    [SerializeField] private GameObject _lookAtObject;

    private Quaternion _initialRotation;
    private float _defaultDelay, _defaultRestartTime;


    void Start()
    {
        if(_lookAt)
            _camera.transform.LookAt(_lookAtObject.transform.position);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _camera.transform.position = _startPosition.position;
        _initialRotation = _camera.transform.rotation;
        _defaultDelay = _delay;
        _defaultRestartTime = _restartTime;
    }

    void Update()
    {
        if (_delay <= 0)
        {
            if (_lookAt)
            {
                _camera.transform.LookAt(_lookAtObject.transform.position);
                _camera.transform.localPosition += _camera.transform.right * _moveSpeed * Time.deltaTime;
            }
            else
            {
                RotateCamera();
                MoveCamera();
            }
        }
        else
        {
            _delay -= Time.deltaTime;
        }

        _restartTime -= Time.deltaTime;
        if(_restartTime <= 0)
        {
            _backToStart = true;
        }

        if (_backToStart)
        {
            _camera.transform.position = _startPosition.position;
            _camera.transform.rotation = _initialRotation;
            _delay = _defaultDelay;
            _restartTime = _defaultRestartTime;
            _backToStart = false;
        }
    }


    private void MoveCamera()
    {
        if(!_backToStart)
        {
            _camera.transform.position = Vector3.LerpUnclamped(_camera.transform.position, _targetPosition.position, _moveSpeed * Time.deltaTime);
        }
    }

    private void RotateCamera()
    {
        if (!_backToStart)
        {
            if (_constantRotation)
                _camera.transform.Rotate(_rotateVector, _rotateSpeed * Time.deltaTime);
            else if(_finiteRotation)
                _camera.transform.localRotation = Quaternion.Lerp(_camera.transform.localRotation, Quaternion.Euler(_rotateVector), _rotateSpeed * Time.deltaTime);
        }
    }
}
