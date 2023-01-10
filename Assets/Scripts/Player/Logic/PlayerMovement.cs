using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 velocity;
    bool isGrounded;


    public void PlayerMove(CharacterController controller, PlayerData playerData, PlayerInput playerInput)
    {
        isGrounded = Physics.CheckSphere(playerData.groundCheck.position, playerData.groundDistance, playerData.groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        Vector3 move = transform.right * playerInput.playerMovementInput.x + transform.forward * playerInput.playerMovementInput.y;

        if(playerInput.playerRunInput)
            controller.Move(move * playerData.runSpeed * Time.deltaTime);
        else
            controller.Move(move * playerData.walkSpeed * Time.deltaTime);


        if (playerInput.playerJumpInput && isGrounded)
        {
            velocity.y = Mathf.Sqrt(playerData.jumpHeight * -2f * playerData.gravity);
        }

        velocity.y += playerData.gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}