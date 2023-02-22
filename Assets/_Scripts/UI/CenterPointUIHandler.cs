using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterPointUIHandler : MonoBehaviour
{
    private bool _itemsCurrentlyVisible;

    //Center point variables
    private float _transparency, _previousTransparency, _maxTransparency = 0.08f, _changeSpeed = 0.1f;
    private Color _centerPointColor;

    //Updates center point when an interactable is in camera view
    public void AreItemsVisible(bool areItemsVisible)
    {
        _itemsCurrentlyVisible = areItemsVisible;
    }

    public void UpdateCenterPoint(Image uiCenterPoint)
    {
        if (_itemsCurrentlyVisible && _transparency < _maxTransparency)
        {
            _transparency += _changeSpeed * 2 * Time.deltaTime;
        }
        else if (!_itemsCurrentlyVisible && _transparency > 0)
        {
            _transparency -= _changeSpeed * Time.deltaTime;
        }

        Mathf.Clamp(_transparency, 0f, _maxTransparency);

        if (_transparency != _previousTransparency) //Prevents color from being passed to the UI manager when not necessary
        {
            _centerPointColor = new Color(1, 1, 1, _transparency);

            uiCenterPoint.color = _centerPointColor;

            _previousTransparency = _transparency;
        }
    }
}
