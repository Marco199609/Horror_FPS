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

    private AudioSource _flickerSource;
    private Light _light;
    private SoundData _soundData;
    private float _lightMaxIntensity, _timer;
    private bool _lightOn;

    private void Awake()
    {
        _light = GetComponent<Light>();
        _lightMaxIntensity = _light.intensity;
        _timer = UnityEngine.Random.Range(_minFlickerDuration, _maxFlickerDuration);
    }

    private void Start()
    {
        if (_soundData == null) _soundData = SoundManager.Instance.SoundData;
        _flickerSource = SoundManager.Instance.CreateModifiableAudioSource(_soundData.LightFlickerClip, _lamp.gameObject, _soundData.LightFlickerClipVolume);
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
                    if(_flickerSource.isPlaying) _flickerSource.Stop();
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
                _light.intensity = UnityEngine.Random.Range(_lightMaxIntensity - 0.2f, _lightMaxIntensity);

                if (!_flickerSource.isPlaying)
                {
                    _flickerSource.volume = _soundData.LightFlickerClipVolume * SoundManager.Instance.GlobalSoundFXVolume;
                    _flickerSource.Play();
                } 
            }
        }
    }
}
