using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    //Sunlight variables
    [SerializeField] private Light _sunlight;
    [SerializeField] private float _sunriseHour, _sunsetHour;
    private TimeSpan _sunriseTime;
    private TimeSpan _sunsetTime;

    //Ambient light variables
    [SerializeField] private Color _dayAmbientLight, _nightAmbientLight;
    [SerializeField] private AnimationCurve _lightChangeCurve;
    [SerializeField] private Light _moonlight;
    [SerializeField] private float _maxSunlightIntensity, _maxMoonlightIntensity;


    //Time variables
    [SerializeField] private float _timeMultiplier, _startHour;
    [SerializeField] private TextMeshProUGUI _timeUIText;
    private DateTime _currentTime;


    private void Start()
    {
        _currentTime = DateTime.Now.Date + TimeSpan.FromHours(_startHour);

        _sunriseTime = TimeSpan.FromHours(_sunriseHour);
        _sunsetTime = TimeSpan.FromHours(_sunsetHour);
    }


    private void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
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

        _sunlight.transform.rotation = Quaternion.AngleAxis(sunlightRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(_sunlight.transform.forward, Vector3.down);
        _sunlight.intensity = Mathf.Lerp(0, _maxSunlightIntensity, _lightChangeCurve.Evaluate(dotProduct));
        _moonlight.intensity = Mathf.Lerp(_maxMoonlightIntensity, 0, _lightChangeCurve.Evaluate(dotProduct));
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
}
