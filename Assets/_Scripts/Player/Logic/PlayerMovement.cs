using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
    [HideInInspector]
    public bool canMove = true;

    bool _isGrounded;
    Vector3 _moveDirection = Vector3.zero;
    PlayerData _playerData;

    public void PlayerMove(GameObject player, IPlayerInput playerInput)
    {
        if (_playerData == null) _playerData = player.GetComponent<PlayerData>();

        _isGrounded = Physics.CheckSphere(_playerData.groundCheck.position, _playerData.groundDistance, _playerData.groundMask);

        // We are grounded, so recalculate move direction based on axes
        //Vector3 forward = player.transform.TransformDirection(Vector3.forward);
        //Vector3 right = player.transform.TransformDirection(Vector3.right);

        //Using cinemachine, this is the implementation
        Vector3 forward = _playerData.Camera.transform.TransformDirection(Vector3.forward);
        Vector3 right = _playerData.Camera.transform.TransformDirection(Vector3.right);

        float curSpeedX = canMove ? (playerInput.playerRunInput ? _playerData.runSpeed : _playerData.walkSpeed) * playerInput.playerMovementInput.y : 0;
        float curSpeedY = canMove ? (playerInput.playerRunInput ? _playerData.runSpeed : _playerData.walkSpeed) * playerInput.playerMovementInput.x : 0;
        float movementDirectionY = _moveDirection.y;
        _moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (playerInput.playerJumpInput && canMove && _isGrounded) _moveDirection.y = _playerData.jumpHeight;
        else _moveDirection.y = movementDirectionY;

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        _moveDirection.y += _playerData.gravity * Time.deltaTime;

        _playerData.characterController.Move(_moveDirection * Time.deltaTime);
    }
}