﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour, IPlayerUI
{
    private float _transparency, _maxTransparency = 0.08f, _changeSpeed = 0.1f;
    public void CenterPointControl(Image UICenterPoint, List<GameObject> itemsVisible)
    {
        if(itemsVisible.Count > 0 && _transparency < _maxTransparency) _transparency += _changeSpeed * 2 * Time.deltaTime;
        else if(itemsVisible.Count <= 0 && _transparency > 0) _transparency -= _changeSpeed * Time.deltaTime;

        Mathf.Clamp(_transparency, 0f, _maxTransparency);

        UICenterPoint.color = new Color(1, 1, 1, _transparency);
    }
}