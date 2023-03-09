using UnityEngine;
public interface IFlashlightControl
{
    void FlashlightControl(PlayerData playerData, IPlayerInput playerInput);
    void AddBattery();
}
