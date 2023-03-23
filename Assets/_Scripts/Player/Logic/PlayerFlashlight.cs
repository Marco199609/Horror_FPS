using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IFlashlightControl
{
    void FlashlightControl(PlayerData playerData, IPlayerInput playerInput);
    void AddBattery();
}

public class PlayerFlashlight : MonoBehaviour, IFlashlightControl
{
    private bool _waitForScroll, _isScrolling, _hasBattery, _turnOn;
    private float _currentIntensity;
    private Light _flashlight;
    private PlayerData _playerData;

    public void FlashlightControl(PlayerData playerData, IPlayerInput playerInput)
    {
        if (_flashlight == null) _flashlight = playerData.Flashlight.GetComponent<Light>();
        if (_playerData == null) _playerData = playerData;

        if (_turnOn) _flashlight.transform.parent.gameObject.SetActive(true);
        else _flashlight.transform.parent.gameObject.SetActive(false);

        if (_hasBattery)
        {
            IntensityControl();
        }
        else
        {
            _flashlight.intensity = 0;
        }
        
        InputControl(playerInput);
    }

    private void IntensityControl()
    {
        //Clamps max intensity to the current energy
        _currentIntensity = Mathf.Clamp(_currentIntensity, _playerData.MinIntensity, _playerData.MaxIntensity); 
        _flashlight.intensity = _currentIntensity; //Sets intensity
    }

    private void InputControl(IPlayerInput playerInput)
    {
        if (playerInput.FlashLightInput) 
            _waitForScroll = true; //While player holds button down, waits for scroll input

        if (playerInput.MouseScrollInput != 0) 
            _isScrolling = true; //While player scrolls, set state to true
        
        if (playerInput.FlashLightInput && _waitForScroll && _isScrolling && _turnOn) //If the player holds the F button and scrolls, the flashlight intensity changes accordingly
        {
            _currentIntensity += playerInput.MouseScrollInput * Time.deltaTime * _playerData.ChangeVelocity;
        } 
        else if(!playerInput.FlashLightInput && _waitForScroll && !_isScrolling) // If the player didn't scroll, this turns the flashlight on or off 
        {
            _turnOn = !_turnOn;

            SoundManager.Instance.Play2DSoundEffect(SoundManager.Instance.FlashlightClip, SoundManager.Instance.FlashlightClipVolume);

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

    public void AddBattery()
    {
        _currentIntensity = _playerData.MaxIntensity - (_playerData.MaxIntensity - _playerData.MinIntensity) / 2; //Sets current intensity in a midpoint
        _hasBattery = true;
    }
}
