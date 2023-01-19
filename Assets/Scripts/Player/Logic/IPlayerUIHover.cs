using UnityEngine;

public interface IPlayerUIHover
{
    void Hover(GameObject player, RaycastHit hit, GameController gameController);
}