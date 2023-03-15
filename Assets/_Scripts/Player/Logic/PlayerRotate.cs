using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerRotate
{
    void RotatePlayer(PlayerData playerData, IPlayerInput playerInput, bool disableCinemachine);
}

public class PlayerRotate : MonoBehaviour, IPlayerRotate
{
    private CinemachinePOV _cinemachinePOV;

    public void RotatePlayer(PlayerData playerData, IPlayerInput playerInput, bool disableCinemachine)
    {
        if (_cinemachinePOV == null) _cinemachinePOV = playerData.VirtualCamera.GetCinemachineComponent<CinemachinePOV>();

        if(disableCinemachine)
        {
            _cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = 0;
            _cinemachinePOV.m_VerticalAxis.m_MaxSpeed = 0;
        }
        else
        {
            _cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = playerData.mouseSensitivityX;
            _cinemachinePOV.m_VerticalAxis.m_MaxSpeed = playerData.mouseSensitivityY;
        }
    }
}
