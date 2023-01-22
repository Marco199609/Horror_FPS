using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IPlayerUI
{
    void CenterPointControl(Image UICenterPoint, List<GameObject> itemsVisible);
}
