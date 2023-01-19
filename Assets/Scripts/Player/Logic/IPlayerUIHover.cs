using UnityEngine;

public interface IPlayerUIHover
{
    void HoverOverItem(GameObject player, RaycastHit hit, GameController gameController);
    void DeactivateUIElements(GameObject player, GameController gameController);
}