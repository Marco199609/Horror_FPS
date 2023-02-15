using UnityEngine;

public interface IPlayerInput
{
    bool FlashLightInput { get; }
    Vector2 mouseMovementInput { get; }
    float MouseScrollInput { get; }
    bool playerJumpInput { get; }
    Vector2 playerMovementInput { get; }
    bool playerPickupInput { get; }
    bool playerRunInput { get; }
}