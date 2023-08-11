using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private CinemachinePOV _vCamCinemachinePOV;

    private void Start()
    {
        _vCamCinemachinePOV = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    public void LoadHouseLevel(PlayerController playerController, Vector3 playerLocalPosition, float playerRotationY)
    {

        SceneManager.LoadScene("Level_House");

        SetPlayerPosition(playerController, playerLocalPosition);
        SetPlayerRotation(playerController, playerRotationY);
    }

    public void LoadDreamLevel(PlayerController playerController, Vector3 playerLocalPosition, float playerRotationY)
    {
        SceneManager.LoadScene("Level_Dream");
        SetPlayerPosition(playerController, playerLocalPosition);
        SetPlayerRotation(playerController, playerRotationY);
    }


    private void SetPlayerPosition(PlayerController playerController, Vector3 playerLocalPosition)
    {
        playerController.FreezePlayerMovement = true;
        playerController.Player.transform.localPosition = new Vector3(playerLocalPosition.x, playerController.Player.transform.localPosition.y, playerLocalPosition.z);
        playerController.FreezePlayerMovement = false;
    }

    private void SetPlayerRotation(PlayerController playerController, float playerRotationY)
    {
        playerController.Player.transform.rotation = Quaternion.Euler(new Vector3(0, 
            playerRotationY, 0)); //Easier to rotate player than cinemachine

        _vCamCinemachinePOV.m_HorizontalAxis.Value = 0;
        _vCamCinemachinePOV.m_VerticalAxis.Value = 0;
    }
}
