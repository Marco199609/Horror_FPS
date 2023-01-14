using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashLight : MonoBehaviour
{
    float rotateVelocity;
    Vector3 rot;
    Transform flashlightTransform, camHolderTransform;

    public void FlashlightControl(GameObject player, PlayerData playerData)
    {

        if (flashlightTransform == null)
            flashlightTransform = playerData.flashlight.transform;
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
        rot.y = player.transform.rotation.eulerAngles.y;
        rot.z = player.transform.rotation.eulerAngles.z;

        //rotates the flashlight 
        if (flashlightTransform.rotation != Quaternion.Euler(rot))
        {

            flashlightTransform.rotation = Quaternion.Lerp(flashlightTransform.rotation, Quaternion.Euler(rot),
                playerData.flashlightRotationSpeed * Time.deltaTime);
        }
    }
}
