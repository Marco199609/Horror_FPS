using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 moveDirection = Vector3.zero;

    bool isGrounded;

    [HideInInspector]
    public bool canMove = true;

    public void PlayerMove(GameObject player, PlayerData playerData, PlayerInput playerInput)
    {
        isGrounded = Physics.CheckSphere(playerData.groundCheck.position, playerData.groundDistance, playerData.groundMask);

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = player.transform.TransformDirection(Vector3.forward);
        Vector3 right = player.transform.TransformDirection(Vector3.right);

        float curSpeedX = canMove ? (playerInput.playerRunInput ? playerData.runSpeed : playerData.walkSpeed) * playerInput.playerMovementInput.x : 0;
        float curSpeedY = canMove ? (playerInput.playerRunInput ? playerData.runSpeed : playerData.walkSpeed) * playerInput.playerMovementInput.y : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (playerInput.playerJumpInput && canMove && isGrounded)
        {
            moveDirection.y = playerData.jumpHeight;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)

        moveDirection.y += playerData.gravity * Time.deltaTime;

        playerData.characterController.Move(moveDirection * Time.deltaTime);
    }
}