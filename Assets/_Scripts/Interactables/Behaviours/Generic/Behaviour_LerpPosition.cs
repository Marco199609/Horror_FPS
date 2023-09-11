using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_LerpPosition : MonoBehaviour, ITrigger
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _movementSpeed = 2;
    [SerializeField] private float _movementDuration = 2;
    [SerializeField] private float _movementDelay;
    [SerializeField] private Vector3 _targetLocalPosition;

    [Header("Debug use only")]
    [SerializeField] private float _currentMovementTimer;
    [SerializeField] private bool _isMoving;

    public void TriggerBehaviour(float triggerDelay, bool isInteracting, bool isInspecting)
    {
        StartCoroutine(Trigger(_movementDelay));
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(triggerDelay);
        _isMoving = true;
        _currentMovementTimer = _movementDuration;
    }

    private void Update()
    {
        if (_isMoving)
        {
            //Moves object
            _gameObject.transform.localPosition = Vector3.Lerp(_gameObject.transform.localPosition, new Vector3(_targetLocalPosition.x, _targetLocalPosition.y, _targetLocalPosition.z),
                    _movementSpeed * Time.deltaTime);

            _currentMovementTimer -= Time.deltaTime;

            if (_currentMovementTimer <= 0)
            {
                _isMoving = false;
                if (PlayerController.Instance.FreezePlayerMovement == true) PlayerController.Instance.FreezePlayerMovement = false;
                if (PlayerController.Instance.FreezePlayerRotation == true) PlayerController.Instance.FreezePlayerRotation = false;
            }
        }
    }
}
