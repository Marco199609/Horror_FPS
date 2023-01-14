using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashLight : MonoBehaviour
{
    float rotateVelocity;
    Vector3 rot;
    Transform flashlightTransform, playerTransform, camHolderTransform;

    public void FlashlightControl(PlayerData playerData)
    {

        if (flashlightTransform == null)
            flashlightTransform = playerData.flashlight.transform;
        if (playerTransform == null)
            playerTransform = playerData.gameObject.transform;
        if (camHolderTransform == null)
            camHolderTransform = playerData.camHolder.transform;


        //sets flashlight position to cam holder position
        if(flashlightTransform.position != camHolderTransform.position)
        {
            flashlightTransform.position = Vector3.Lerp(flashlightTransform.position, camHolderTransform.position,
                playerData.flashlightRotationSpeed * Time.deltaTime * 0.8f);
        }

        //gets rotation for flashlight
        rot.x = camHolderTransform.rotation.eulerAngles.x;
        rot.y = playerTransform.rotation.eulerAngles.y;
        rot.z = playerTransform.rotation.eulerAngles.z;

        //rotates the flashlight 
        if (flashlightTransform.rotation != Quaternion.Euler(rot))
        {

            flashlightTransform.rotation = Quaternion.Lerp(flashlightTransform.rotation, Quaternion.Euler(rot),
                playerData.flashlightRotationSpeed * Time.deltaTime);
        }
    }
}
