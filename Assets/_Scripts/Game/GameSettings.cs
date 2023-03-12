using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class GameSettings : MonoBehaviour
{
    public bool Pause;

    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private GameObject _mainMenuCanvas;

    [Header("Game Settings")]
    [SerializeField] private TMP_Dropdown _framerateDropdownButton;
    [SerializeField] private Slider _mouseSensitivitySlider;
    [SerializeField] private Toggle _vSyncToggle;

    private float _mouseSensitivity = 200;
    private int _targetFramerate;
    private bool _inGame;

    private PlayerData _playerData;

    private void Awake()
    {
        SetFramerate();
        SetMouseSensitivity();
        SetVsync();
    }

    #region Canvas Control
    public void OpenSettings()
    {
        _settingsCanvas.SetActive(true);
        if(!_inGame) _mainMenuCanvas.SetActive(false);
    }

    public void CloseSettings()
    {
        _settingsCanvas.SetActive(false);
        if (!_inGame) _mainMenuCanvas.SetActive(true);
        else
        {
            Pause = false;
        } 
    }
    #endregion

    #region Game Settings
    public void SetMouseSensitivity()
    {
        _mouseSensitivity = _mouseSensitivitySlider.value;
    }
    

    public void SetFramerate()
    {
        if (_framerateDropdownButton.value == 0) _targetFramerate = 30;
        else if (_framerateDropdownButton.value == 1) _targetFramerate = 60;
        else if (_framerateDropdownButton.value == 2) _targetFramerate = 144;
        else if (_framerateDropdownButton.value == 3) _targetFramerate = 0;

        Application.targetFrameRate = _targetFramerate;
    }

    public void SetVsync()
    {
        if (_vSyncToggle.isOn) QualitySettings.vSyncCount = 1;
        else QualitySettings.vSyncCount = 0;
    }

    #endregion

    private void Update()
    {
        if(_playerData == null)
        {
            _playerData = FindObjectOfType<PlayerData>();
        }

        if (_playerData != null)
        {
            _playerData.mouseSensitivityX = _mouseSensitivity;
            _playerData.mouseSensitivityY = _mouseSensitivity;
        }


        if (!_inGame)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (SceneManager.GetActiveScene().name == "Level0") _inGame = true;
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Pause = !Pause;
            }
        }

        if(_inGame && Pause)
        {
            _settingsCanvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(_inGame && !Pause)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
