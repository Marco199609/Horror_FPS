using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [Header("Sunlight")]
    [SerializeField] private Light _sun;
    [SerializeField] private float _sunriseHour, _sunsetHour;
    private TimeSpan _sunriseTime;
    private TimeSpan _sunsetTime;

    [Header("Ambient Light")]
    [SerializeField] private Color _dayAmbientLight;
    [SerializeField] private Color _nightAmbientLight;
    [SerializeField] private AnimationCurve _lightChangeCurve;
    //[SerializeField] private Light _moonlight;
    [SerializeField] private float _maxSunlightIntensity; //, _maxMoonlightIntensity;


    [Header("Time Characteristics")]
    [SerializeField] private float _timeMultiplier;
    [SerializeField] private float _startHour;
    private DateTime _currentTime;

    [Header("Clock game object")]
    [SerializeField] private GameObject _clock;
    [SerializeField] private Transform _hourHand, _minuteHand;
    private PlayerController _playerController;



    private void Start()
    {
        _currentTime = DateTime.Now.Date + TimeSpan.FromHours(_startHour);

        _sunriseTime = TimeSpan.FromHours(_sunriseHour);
        _sunsetTime = TimeSpan.FromHours(_sunsetHour);

        _playerController = FindObjectOfType<PlayerController>();
    }


    private void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        ClockControl();
    }

    private void UpdateTimeOfDay()
    {
        _currentTime = _currentTime.AddSeconds(Time.deltaTime * _timeMultiplier);
    }

    private void RotateSun()
    {
        float sunlightRotation;

        if(_currentTime.TimeOfDay > _sunriseTime && _currentTime.TimeOfDay < _sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(_sunriseTime, _sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(_sunriseTime, _currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunlightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseRotation = CalculateTimeDifference(_sunsetTime, _sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(_sunsetTime, _currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseRotation.TotalMinutes;
            sunlightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        _sun.transform.rotation = Quaternion.AngleAxis(sunlightRotation, Vector3.right);

    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(_sun.transform.forward, Vector3.down);
        _sun.intensity = Mathf.Lerp(0, _maxSunlightIntensity, _lightChangeCurve.Evaluate(dotProduct));
        //_moonlight.intensity = Mathf.Lerp(_maxMoonlightIntensity, 0, _lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(_nightAmbientLight, _dayAmbientLight, _lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;
        if(difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }


    private void ClockControl()
    {
        float clockHandRotation, hourHandRotationsInADay = 2;
        var totalMinutesInADay = 1440;

        TimeSpan timeDifference = CalculateTimeDifference(TimeSpan.FromHours(0), _currentTime.TimeOfDay);

        if (timeDifference.TotalMinutes >= totalMinutesInADay + 1) timeDifference = TimeSpan.FromMinutes(0);

        var percentage = timeDifference.TotalMinutes / totalMinutesInADay;

        clockHandRotation = Mathf.Lerp(0, 360, (float)percentage);

        /*if(_clock.gameObject != _playerController.SelectedInventoryItem)
        {
            _clock.MinuteHand.localRotation = Quaternion.AngleAxis(clockHandRotation * (1440 / 60) * minuteHandRotationsPerHour, -Vector3.up);
        }
        else //Enables realistic clock movement when clock selected
        {
            _clock.MinuteHand.localRotation = Quaternion.AngleAxis(clockHandRotation * 1440 / _timeMultiplier, -Vector3.up);
        }

        _clock.HourHand.localRotation = Quaternion.AngleAxis(clockHandRotation * hourHandRotationsInADay, -Vector3.up);*/

        if (_clock == _playerController.Inventory.SelectedItem())
        {
            _minuteHand.localRotation = Quaternion.AngleAxis(clockHandRotation * 1440 / _timeMultiplier, -Vector3.up);
            _hourHand.localRotation = Quaternion.AngleAxis(clockHandRotation * hourHandRotationsInADay, -Vector3.up);
        }
    }
}
