using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour, IPlayerUI
{
    private float _transparency, _maxTransparency = 0.08f, _changeSpeed = 0.1f;
    private Color _centerPointColor;

    public delegate void UpdateUI(Color color);
    public static event UpdateUI ColorUpdated;

    public void CenterPointControl(List<GameObject> itemsVisible)
    {
        if(itemsVisible.Count > 0 && _transparency < _maxTransparency) _transparency += _changeSpeed * 2 * Time.deltaTime;
        else if(itemsVisible.Count <= 0 && _transparency > 0) _transparency -= _changeSpeed * Time.deltaTime;

        Mathf.Clamp(_transparency, 0f, _maxTransparency);

        _centerPointColor = new Color(1, 1, 1, _transparency);

        ColorUpdated?.Invoke(_centerPointColor);
    }
}