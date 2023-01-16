using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashLight : MonoBehaviour
{
    //float rotateVelocity;
    Light flashlight;
    

    public void FlashlightControl(PlayerData playerData, PlayerInput playerInput)
    {
        if (playerInput.FlashLightInput)
        {
            if (flashlight == null)
                flashlight = playerData.flashlight.GetComponent<Light>();

            flashlight.intensity += playerInput.MouseScrollInput * 7 * Time.deltaTime;
            flashlight.intensity = Mathf.Clamp(flashlight.intensity, 1, 5);

            if (flashlight.intensity < 1.2f)
                flashlight.gameObject.SetActive(false);
            else
                flashlight.gameObject.SetActive(true);
        }
    }
}
