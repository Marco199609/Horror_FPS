using UnityEngine;

public interface IPlayerMovement
{
    void PlayerMove(GameObject player, IPlayerInput playerInput);
}