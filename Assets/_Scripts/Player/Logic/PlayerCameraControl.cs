using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ICameraControl
{
    void ControlCameraHeadBob(GameObject player, IPlayerInput playerInput);
}

public class PlayerCameraControl : MonoBehaviour, ICameraControl
{
    public void ControlCameraHeadBob(GameObject player, IPlayerInput playerInput)
    {

        //May create motion sickness
        /*
        if (SceneManager.GetActiveScene().name == "Level_Dream")
        {
            float FOV = player.GetComponent<PlayerData>().VirtualCamera.m_Lens.FieldOfView;
            if (playerInput.playerRunInput)
            {
                player.GetComponent<PlayerData>().VirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(FOV, 50, 2 * Time.deltaTime);
            }
            else if (playerInput.playerMovementInput.magnitude > 0.1f)
            {
                player.GetComponent<PlayerData>().VirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(FOV, 47.5f, 2 * Time.deltaTime);
            }
            else
            {
                player.GetComponent<PlayerData>().VirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(FOV, 45, 2 * Time.deltaTime);
            }
        }
        else
            player.GetComponent<PlayerData>().VirtualCamera.m_Lens.FieldOfView = 45;*/
    }
}