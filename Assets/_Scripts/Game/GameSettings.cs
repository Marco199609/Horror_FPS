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
    [SerializeField] private GameObject _startGameButton;
    [SerializeField] private GameObject _continueGameButton;

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

    public void StartGame()
    {
        SceneManager.LoadScene("Level0", LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Level0")
        {
            _inGame = true;
            _playerData = FindObjectOfType<PlayerData>();
            _settingsCanvas.SetActive(false);
            _mainMenuCanvas.SetActive(false);
        }
    }

    public void ContinueGame()
    {
        Pause = false;
        _mainMenuCanvas.SetActive(false);
    }

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

    public void ExitGame()
    {
        Application.Quit();
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

    #region Pause Control
    private void PauseControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pause) ContinueGame();
            else
            {
                Pause = true;
                _mainMenuCanvas.SetActive(true);
            }
        }

        if (Pause)
        {
            if (!_continueGameButton.activeInHierarchy) _continueGameButton.SetActive(true);
            if (_startGameButton.activeInHierarchy) _startGameButton.SetActive(false);

            _playerData.mouseSensitivityX = 0;
            _playerData.mouseSensitivityY = 0;
        }
        else
        {
            _playerData.mouseSensitivityX = _mouseSensitivity;
            _playerData.mouseSensitivityY = _mouseSensitivity;
        }
    }

    private void CursorControl()
    {
        if (Pause)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    #endregion

    private void Update()
    {
        if(_inGame)
        {
            PauseControl();
            CursorControl();
        }
        else
        {
           if(_continueGameButton.activeInHierarchy) _continueGameButton.SetActive(false);
           if(!_startGameButton.activeInHierarchy) _startGameButton.SetActive(true);
        }
    }
}