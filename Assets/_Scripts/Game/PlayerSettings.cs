using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private GameObject _mainMenuCanvas;

    [Header("Player Settings")]
    [SerializeField] private float _mouseXSensitivity = 10;
    [SerializeField] private float _mouseYSensitivity = 10;

    private PlayerData _playerData;

    #region Canvas Control
    public void OpenSettings()
    {
        _settingsCanvas.SetActive(true);
        _mainMenuCanvas.SetActive(false);
    }

    public void CloseSettings()
    {
        _settingsCanvas.SetActive(false);
        _mainMenuCanvas.SetActive(true);
    }
    #endregion

    #region Mouse Sensitivity
    public void ChangeVerticalSensitivity()
    {
        _mouseYSensitivity = EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value;

        print(_mouseYSensitivity);
    }
    public void ChangeHorizontalSensitivity()
    {
        _mouseXSensitivity = EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value;
    }
    #endregion

    private void Update()
    {
        if(_playerData == null)
        {
            _playerData = FindObjectOfType<PlayerData>();

            if(_playerData != null)
            {
                _playerData.mouseSensitivityX = _mouseXSensitivity;
                _playerData.mouseSensitivityY = _mouseYSensitivity;
            }
        }    
    }
}
