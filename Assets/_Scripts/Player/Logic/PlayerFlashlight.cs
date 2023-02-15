using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFlashlight : MonoBehaviour, IFlashlightControl
{
    private bool _waitForScroll, _isScrolling;
    private float _previousIntensity;
    private Light _flashlight, _weaponlight;
    private PlayerData _playerData;

    private void Awake()
    {
        //Adds this object to the object manager for future use
        //ObjectManager.Instance.PlayerFlashlight = this;
    }
    public void FlashlightControl(PlayerData playerData, IPlayerInput playerInput)
    {
        if (_flashlight == null) _flashlight = playerData.Flashlight.GetComponent<Light>();
        if (_weaponlight == null) _weaponlight = playerData.WeaponLight.GetComponent<Light>();
        if (_playerData == null) _playerData = playerData;

        InputControl(playerInput);
        IntensityControl(playerInput);
        WeaponLightControl();
    }

    private void WeaponLightControl()
    {
        _weaponlight.intensity = _playerData.CurrentIntensity;
        _weaponlight.intensity = Mathf.Clamp(_weaponlight.intensity, 1.5f, 5);
    }

    private void IntensityControl(IPlayerInput playerInput)
    {
        //Depletes the battery faster if intensity higher
        if(_flashlight.gameObject.activeInHierarchy) _playerData.CurrentEnergy -= Time.deltaTime * _playerData.CurrentIntensity * _playerData.DepletionSpeed; //Only depletes battery if flashlight on

        //Clamps max intensity to the current energy
        _playerData.CurrentIntensity = Mathf.Clamp(_playerData.CurrentIntensity, _playerData.MinIntensity, _playerData.MaxIntensity * (_playerData.CurrentEnergy / 100)); 

        //Powers off flashlight if the intensity is below a certain limit
        if (_playerData.CurrentIntensity < _playerData.SwitchOnLimit) _flashlight.gameObject.SetActive(false);
        else _flashlight.gameObject.SetActive(true);

        _flashlight.intensity = _playerData.CurrentIntensity; //Sets intensity
    }

    private void InputControl(IPlayerInput playerInput)
    {
        if (playerInput.FlashLightInput) _waitForScroll = true; //While player holds button down, waits for scroll input
        if (playerInput.MouseScrollInput != 0) _isScrolling = true; //While player scrolls, set state to true
        
        if (playerInput.FlashLightInput && _waitForScroll && _isScrolling) //If the player holds the F button and scrolls, the flashlight intensity changes accordingly
        {
            _playerData.CurrentIntensity += playerInput.MouseScrollInput * Time.deltaTime * _playerData.ChangeVelocity;
        } 
        else if(!playerInput.FlashLightInput && _waitForScroll && !_isScrolling) // If the player didn't scroll, this turns the flashlight on or off 
        {
            if (_playerData.CurrentIntensity < _playerData.SwitchOnLimit)
            {
                if(_previousIntensity > _playerData.MinIntensity) _playerData.CurrentIntensity = _previousIntensity; //If there's a previous intensity stored, turns on flashlight whith that intensity
                else _playerData.CurrentIntensity = _playerData.MinIntensity + ((_playerData.MaxIntensity - _playerData.MinIntensity) / 2); //If no previous intensity stored, turns on flaslight on medium intensity
            }
            else
            {
                _previousIntensity = _playerData.CurrentIntensity; //Saves current intensity to use when the flashlight is turned on again
                _playerData.CurrentIntensity = _playerData.MinIntensity; //If flashlight on, turns off
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
    }

    //Items use this to add energy to the player's flashlight
    public void AddEnergy(float energy)
    {
        if ((_playerData.CurrentEnergy + energy) < _playerData.MaxEnergy) _playerData.CurrentEnergy += energy;
        else _playerData.CurrentEnergy = _playerData.MaxEnergy;

        /*
        //If flashlight off, turn on when battery used from inventory
        if (_currentIntensity < _switchOnLimit) _currentIntensity = _minIntensity + ((_maxIntensity - _minIntensity) / 3); 

        if ((_currentEnergy + energy) < _maxEnergy) _currentEnergy += energy;
        else _currentEnergy = _maxEnergy;*/
    }
}
