using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IPlayerUI
{
    void InteractableUI(PlayerData playerData, RaycastHit hit);
    void CenterPointControl(List<GameObject> itemsVisible);
}
