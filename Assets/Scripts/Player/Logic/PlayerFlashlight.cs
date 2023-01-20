using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashlight : MonoBehaviour, IFlashlightControl
{
    public void FlashlightControl(Light flashlight, Light weaponLight, PlayerInput playerInput)
    {
        if (playerInput.FlashLightInput)
        {
            flashlight.intensity += playerInput.MouseScrollInput * 7 * Time.deltaTime;
            flashlight.intensity = Mathf.Clamp(flashlight.intensity, 1, 5);

            if (flashlight.intensity < 1.2f)
                flashlight.gameObject.SetActive(false);
            else
                flashlight.gameObject.SetActive(true);
        }
        weaponLight.intensity = flashlight.intensity;
        weaponLight.intensity = Mathf.Clamp(weaponLight.intensity, 2f, 5);
    }
}
