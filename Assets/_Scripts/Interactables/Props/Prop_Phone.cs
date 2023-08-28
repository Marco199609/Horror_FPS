using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Phone : MonoBehaviour
{
    [SerializeField] private float _ringDelay, _ringInterval;
    [SerializeField] private AudioClip _phoneRingClip;

    private AudioSource _phoneSource;
    private bool _playerStressAdded;

    void Start()
    {
        _phoneSource = GetComponent<AudioSource>();

        InvokeRepeating("RingPhone", _ringDelay, _ringInterval);
    }

    private void RingPhone()
    {
        if (!_playerStressAdded)
        {
            PlayerController.Instance.StressControl.AddStress();
            _playerStressAdded = true;
        }

        _phoneSource.PlayOneShot(_phoneRingClip);
    }
}