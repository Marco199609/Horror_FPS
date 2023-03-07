using UnityEngine;

public interface IPlayerRotate
{
    void RotatePlayer(PlayerData playerData, IPlayerInput playerInput, bool disableCinemachine);
}