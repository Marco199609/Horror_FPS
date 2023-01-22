using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFlashlight : MonoBehaviour, IFlashlightControl
{
    private float _minIntensity = 1, _maxIntensity = 5, _switchOnLimit = 1.2f, _changeVelocity = 7; //Intensity variables
    private float _maxEnergy = 100, _currentEnergy, _depletionSpeed = 0.3f; //Energy variables

    private void Awake()
    {
        //Adds this object to the object manager for future use
        ObjectManager.Instance.PlayerFlashlight = this;
    }
    public void FlashlightControl(Light flashlight, Light weaponLight, PlayerInput playerInput)
    {
        IntensityControl(flashlight, playerInput);
        WeaponLightControl(flashlight, weaponLight);
    }

    private void WeaponLightControl(Light flashlight, Light weaponLight)
    {
        weaponLight.intensity = flashlight.intensity;
        weaponLight.intensity = Mathf.Clamp(weaponLight.intensity, 2f, 5);
    }

    private void IntensityControl(Light flashlight, PlayerInput playerInput)
    {
        //Depletes the battery faster if intensity higher
        if(flashlight.gameObject.activeInHierarchy) _currentEnergy -= Time.deltaTime * flashlight.intensity * _depletionSpeed; //Only depletes battery if flashlight on

        //Revieves mouse scroll input
        if (playerInput.FlashLightInput) flashlight.intensity += playerInput.MouseScrollInput * Time.deltaTime * _changeVelocity;

        //Clamps max intensity to the current energy
        flashlight.intensity = Mathf.Clamp(flashlight.intensity, _minIntensity, _maxIntensity * (_currentEnergy / 100)); 

        //Powers off flashlight if the intensity is below a certain limit
        if (flashlight.intensity < _switchOnLimit) flashlight.gameObject.SetActive(false);
        else flashlight.gameObject.SetActive(true);
    }

    //Items use this to add energy to the player's flashlight
    public void AddEnergy(float energy)
    {
        if ((_currentEnergy + energy) < _maxEnergy) _currentEnergy += energy;
        else _currentEnergy = _maxEnergy;
    }
}
