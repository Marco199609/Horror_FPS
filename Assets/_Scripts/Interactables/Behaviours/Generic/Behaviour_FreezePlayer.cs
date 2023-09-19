using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_FreezePlayer : MonoBehaviour, ITrigger
{
    [SerializeField] private bool _freezePlayerRotation;
    [SerializeField] private bool _freezePlayerMovement;

    private PlayerController _playerController;
    public void TriggerBehaviour(float triggerDelay, bool isInteracting, bool isInspecting)
    {
        if(_playerController == null) _playerController = PlayerController.Instance;

        if (_freezePlayerMovement) _playerController.FreezePlayerMovement = true;
        if (_freezePlayerRotation) _playerController.FreezePlayerRotation = true;
    }
}
