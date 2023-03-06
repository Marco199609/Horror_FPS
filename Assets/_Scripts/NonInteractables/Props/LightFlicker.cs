using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private bool _enableFlicker;
    [SerializeField] private float _minFlickerDuration, _maxFlickerDuration;
    [SerializeField] private Material _emisiveMaterial, _opaqueMaterial;
    [SerializeField] private Renderer _lamp;
    [SerializeField] private Light _light;
    [SerializeField] private AudioSource _audioSource;

    private float _lightMaxIntensity, _timer;
    private bool _lightOn;


    private void Awake()
    {
        _lightMaxIntensity = _light.intensity;
        _timer = UnityEngine.Random.Range(_minFlickerDuration, _maxFlickerDuration);
    }

    private void Update()
    {
        if(_enableFlicker)
        {
            _timer -= Time.deltaTime;

            if (_timer < 0)
            {
                if (_lightOn)
                {
                    if(_audioSource.isPlaying) _audioSource.Stop();
                    _light.enabled = false;
                    _lamp.material = _opaqueMaterial;
                    _timer = UnityEngine.Random.Range(_minFlickerDuration, _maxFlickerDuration);
                    _lightOn = false;
                }
                else
                {
                    _light.enabled = true;
                    _lamp.material = _emisiveMaterial;
                    _timer = UnityEngine.Random.Range(_minFlickerDuration, _maxFlickerDuration);
                    _lightOn = true;
                }
            }

            if (_lightOn)
            {
                _light.intensity = UnityEngine.Random.Range(_lightMaxIntensity - 0.05f, _lightMaxIntensity);
                if(!_audioSource.isPlaying) _audioSource.Play();
            }
        }
    }
}
