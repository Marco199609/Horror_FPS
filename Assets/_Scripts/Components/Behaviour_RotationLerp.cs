using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_RotationLerp : MonoBehaviour, ITrigger
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _rotationDuration;
    [SerializeField] private float _rotationDelay;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _isRotating;
    [SerializeField] private Vector3 _targetRotation;

    [Header("Debug use only")]
    [SerializeField] private float _currentRotationTimer;

    public void TriggerBehaviour(float triggerDelay)
    {
        StartCoroutine(Trigger(_rotationDelay));
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(triggerDelay);
        _isRotating = true;
        _currentRotationTimer = _rotationDuration;
    }

    private void Update()
    {
        if(_isRotating)
        {
            //Rotates object
            _gameObject.transform.localRotation = Quaternion.Lerp(_gameObject.transform.localRotation, Quaternion.Euler(_targetRotation),
                    _rotationSpeed * Time.deltaTime);

            _currentRotationTimer -= Time.deltaTime;

            if(_currentRotationTimer <= 0)
            {
                _isRotating = false;
                if(PlayerController.Instance.FreezePlayerMovement == true) PlayerController.Instance.FreezePlayerMovement = false;
                if(PlayerController.Instance.FreezePlayerRotation == true) PlayerController.Instance.FreezePlayerRotation = false;
            }
        }
    }
}
