using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFlashlight : MonoBehaviour, IFlashlightControl
{
    private float _currentIntensity, _minIntensity = 1, _maxIntensity = 5, _previousIntensity, _switchOnLimit = 1.05f, _changeVelocity = 7; //Intensity variables
    private float _maxEnergy = 100, _currentEnergy, _depletionSpeed = 0.3f; //Energy variables

    private bool _waitForScroll, _isScrolling;

    private void Awake()
    {
        //Adds this object to the object manager for future use
        ObjectManager.Instance.PlayerFlashlight = this;
    }
    public void FlashlightControl(Light flashlight, Light weaponLight, PlayerInput playerInput)
    {
        InputControl(flashlight, playerInput);
        IntensityControl(flashlight, playerInput);
        WeaponLightControl(flashlight, weaponLight);
    }

    private void WeaponLightControl(Light flashlight, Light weaponLight)
    {
        weaponLight.intensity = _currentIntensity;
        weaponLight.intensity = Mathf.Clamp(weaponLight.intensity, 2f, 5);
    }

    private void IntensityControl(Light flashlight, PlayerInput playerInput)
    {
        //Depletes the battery faster if intensity higher
        if(flashlight.gameObject.activeInHierarchy) _currentEnergy -= Time.deltaTime * _currentIntensity * _depletionSpeed; //Only depletes battery if flashlight on

        //Clamps max intensity to the current energy
        _currentIntensity = Mathf.Clamp(_currentIntensity, _minIntensity, _maxIntensity * (_currentEnergy / 100)); 

        //Powers off flashlight if the intensity is below a certain limit
        if (_currentIntensity < _switchOnLimit) flashlight.gameObject.SetActive(false);
        else flashlight.gameObject.SetActive(true);

        flashlight.intensity = _currentIntensity; //Sets intensity
    }

    private void InputControl(Light flashlight, PlayerInput playerInput)
    {
        if (playerInput.FlashLightInput) _waitForScroll = true; //While player holds button down, waits for scroll input
        if (playerInput.MouseScrollInput != 0) _isScrolling = true; //While player scrolls, set state to true
        
        if (playerInput.FlashLightInput && _waitForScroll && _isScrolling) //If the player holds the F button and scrolls, the flashlight intensity changes accordingly
        {
            _currentIntensity += playerInput.MouseScrollInput * Time.deltaTime * _changeVelocity;
        } 
        else if(!playerInput.FlashLightInput && _waitForScroll && !_isScrolling) // If the player didn't scroll, this turns the flashlight on or off 
        {
            if (_currentIntensity < _switchOnLimit)
            {
                if(_previousIntensity > _minIntensity) _currentIntensity = _previousIntensity; //If there's a previous intensity stored, turns on flashlight whith that intensity
                else _currentIntensity = _minIntensity + ((_maxIntensity - _minIntensity) / 2); //If no previous intensity stored, turns on flaslight on medium intensity
            }
            else
            {
                _previousIntensity = _currentIntensity; //Saves current intensity to use when the flashlight is turned on again
                _currentIntensity = _minIntensity; //If flashlight on, turns off
            }

            //Resets bools if player input no longer pressing button
            _waitForScroll = false;
            _isScrolling= false;
        }
        else if(!playerInput.FlashLightInput && _waitForScroll && _isScrolling) //If player releases button, resets bools
        {
            _waitForScroll = false;
            _isScrolling = false;
        }

        print(_currentIntensity);
    }

    //Items use this to add energy to the player's flashlight
    public void AddEnergy(float energy)
    {
        //If flashlight off, turn on when battery used from inventory
        if (_currentIntensity < _switchOnLimit) _currentIntensity = _minIntensity + ((_maxIntensity - _minIntensity) / 3); 

        if ((_currentEnergy + energy) < _maxEnergy) _currentEnergy += energy;
        else _currentEnergy = _maxEnergy;
    }
}
