using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStressControl : MonoBehaviour
{
    [SerializeField] private float _minStessLevel = 1, _maxStressLevel = 2f;
    [SerializeField] private float _currentStressLevel, _targetStressLevel, _stressLowerSpeed = 0.1f;

    private bool _addStress;

    public void ManageStress()
    {
        if (_addStress && _currentStressLevel >= _maxStressLevel - 0.05f)
        {
            _targetStressLevel = _minStessLevel;
            _addStress = false;
        }

        if (_currentStressLevel < _targetStressLevel) _currentStressLevel = Mathf.Lerp(_currentStressLevel, _targetStressLevel, Time.deltaTime * 5);
        else _currentStressLevel = Mathf.Lerp(_currentStressLevel, _targetStressLevel, _stressLowerSpeed * Time.deltaTime);

        _currentStressLevel = Mathf.Clamp(_currentStressLevel, _minStessLevel, _maxStressLevel);
    }

    public void AddStress()
    {
        _targetStressLevel = _maxStressLevel;
        _addStress = true;
    }

    public float StressLevel()
    {
        return _currentStressLevel;
    }
}
