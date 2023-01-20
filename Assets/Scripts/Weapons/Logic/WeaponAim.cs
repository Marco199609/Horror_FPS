using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour, IWeaponAim
{
    private Vector3 _weaponDefaultPosition, _weaponAimPosition, _weaponCurrentPosition;
    private Vector3 _weaponDefaultRotation, _weaponAimRotation, _weaponCurrentRotation;

    private float _speed = 20;

    private void Start()
    {
        _weaponDefaultPosition = new Vector3(0.29180485f, -0.970000029f, 2.99842048f);
        _weaponDefaultRotation = new Vector3(287.334839f, 350.67688f, 94.4530716f);

        _weaponAimPosition = new Vector3(0, -0.5f, 2.8f);
        _weaponAimRotation = new Vector3(-90f, 90f, 0f);
    }
    public void Aim(WeaponData currentWeaponData, WeaponInput weaponInput)
    {
        if(weaponInput.aimInput)
        {
            _weaponCurrentPosition = _weaponAimPosition;
            _weaponCurrentRotation = _weaponAimRotation;
        }
        else
        {
            _weaponCurrentPosition = _weaponDefaultPosition;
            _weaponCurrentRotation = _weaponDefaultRotation;
        }

        LerpPositionAndRotation(currentWeaponData);
    }

    private void LerpPositionAndRotation(WeaponData currentWeaponData)
    {
        //sets current object position to followed object position
        currentWeaponData.WeaponModel.transform.localPosition = Vector3.Lerp(currentWeaponData.WeaponModel.transform.localPosition, _weaponCurrentPosition, _speed * Time.deltaTime);
        currentWeaponData.WeaponModel.transform.localRotation = Quaternion.Lerp(currentWeaponData.WeaponModel.transform.localRotation, Quaternion.Euler(_weaponCurrentRotation),
                _speed * Time.deltaTime);
    }
}
