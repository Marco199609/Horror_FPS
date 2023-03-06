using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private float _timer = 1f;
    [SerializeField] private Material _emisiveMaterial, _opaqueMaterial;
    [SerializeField] private Renderer _lamp;

    private float _lightMaxIntensity;
    private bool _lightOn;
    private Light _light;

    private void Awake()
    {
        _light = GetComponent<Light>();
        _lightMaxIntensity = _light.intensity;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if(_timer < 0 )
        {
            if (_lightOn)
            {
                _light.enabled = false;
                _lamp.material = _opaqueMaterial;
                _timer = UnityEngine.Random.Range(0.5f, 1.5f);
                _lightOn = false;
            }
            else
            {
                _light.enabled = true;
                _lamp.material = _emisiveMaterial;
                _timer = UnityEngine.Random.Range(0.5f, 1.5f);
                _lightOn = true;
            }
        }

        if (_lightOn)
        {
            _light.intensity = UnityEngine.Random.Range(_lightMaxIntensity - 0.05f, _lightMaxIntensity);
        }
    }
}
