using UnityEngine;

public interface IPlayerInspect
{
    void Inspect(Transform inspectable);
    bool Inspecting();
    void ManageInspection(PlayerData playerData, IPlayerInput playerInput);
}