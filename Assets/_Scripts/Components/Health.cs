using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth, _currentHealth;

    public void ModifyHealth(int _healthDifference)
    {
        _currentHealth += _healthDifference;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }
}
