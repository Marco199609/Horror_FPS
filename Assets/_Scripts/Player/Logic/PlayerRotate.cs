using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour, IPlayerRotate
{

    private float _rotationX = 0F;
    private float _rotationY = 0F;
    private float _minimumY = -89F;
    private float _maximumY = 89F;

    private PlayerData _playerData;
    private Transform _camHolder;

    public void RotatePlayer(GameObject player, IPlayerInput playerInput)
    {
        //player.GetComponent<PlayerData>().VirtualCamera.
    }
}
