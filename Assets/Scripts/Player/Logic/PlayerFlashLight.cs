using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashLight : MonoBehaviour
{
    //float rotateVelocity;
    Light flashlight;
    Vector3 rot;
    Transform flashlightTransform, camHolderTransform;
    

    public void FlashlightControl(GameObject player, PlayerData playerData, PlayerInput playerInput)
    {
        FlashlightPositionAndRotation(player, playerData);
        FlashlightAction(playerData, playerInput);
    }

    private void FlashlightPositionAndRotation(GameObject player, PlayerData playerData)
    {
        if (flashlightTransform == null)
            flashlightTransform = playerData.flashlight.transform;
        if (camHolderTransform == null)
            camHolderTransform = playerData.camHolder.transform;


        //sets flashlight position to cam holder position
        if (flashlightTransform.position != camHolderTransform.position)
        {
            flashlightTransform.position = Vector3.Lerp(flashlightTransform.position, camHolderTransform.position,
                playerData.flashlightRotationSpeed * Time.deltaTime * 0.8f);
        }

        //gets rotation for flashlight
        rot.x = camHolderTransform.rotation.eulerAngles.x;
        rot.y = playerData.camTransform.rotation.eulerAngles.y; //player.transform.rotation.eulerAngles.y;
        rot.z = playerData.camTransform.rotation.eulerAngles.z; //player.transform.rotation.eulerAngles.z;

        //rotates the flashlight 
        if (flashlightTransform.rotation != Quaternion.Euler(rot))
        {

            flashlightTransform.rotation = Quaternion.Lerp(flashlightTransform.rotation, Quaternion.Euler(rot),
                playerData.flashlightRotationSpeed * Time.deltaTime);
        }
    }

    private void FlashlightAction(PlayerData playerData, PlayerInput playerInput)
    {
        if(playerInput.FlashLightInput)
        {
            if (flashlight == null)
                flashlight = flashlightTransform.GetComponent<Light>();

            flashlight.intensity += playerInput.MouseScrollInput * 2;
            flashlight.intensity = Mathf.Clamp(flashlight.intensity, 1, 5);

            if (flashlight.intensity < 1.2f)
                flashlight.gameObject.SetActive(false);
            else
                flashlight.gameObject.SetActive(true);
        }
    }
}
