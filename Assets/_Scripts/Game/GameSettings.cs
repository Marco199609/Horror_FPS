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
    public static GameSettings Instance;
    public bool Pause;
    public bool InGame { get; private set; }

    [SerializeField] private TMP_Dropdown _framerateDropdownButton, _languageDropdownButton;
    [SerializeField] private Slider _mouseSensitivitySlider;
    [SerializeField] private Toggle _vSyncToggle, _subtitlesToggle;
    [SerializeField] private PlayerData _playerData;

    private float _mouseSensitivity = 200;
    private int _targetFramerate;
    private bool english, spanish;
    private AudioSource _mainMusicSource;
    private SoundData _soundData;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        SetFramerate();
        SetMouseSensitivity();
        SetVsync();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Level_Dream")
        {
            InGame = true;
            UIManager.Instance.SettingsCanvas.SetActive(false);
            UIManager.Instance.MainMenuCanvas.SetActive(false);
        }
    }

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

    public void SetLanguage()
    {
        if (_languageDropdownButton.captionText.text == "English") // value == 0)
        {
            english = true;
            spanish = false;
        }
        else if (_languageDropdownButton.captionText.text == "Spanish")// == 1)
        {
            english = false;
            spanish = true;
        }

        SetSubtitles();
    }

    public void SetSubtitles()
    {
        if (_subtitlesToggle.isOn)
        {
            if (english)
            {
                DialogueSystem.Instance.DialogueData.English = true;
                DialogueSystem.Instance.DialogueData.Spanish = false;
            }
            if (spanish)
            {
                DialogueSystem.Instance.DialogueData.English = false;
                DialogueSystem.Instance.DialogueData.Spanish = true;
            }
        }
        else
        {
            DialogueSystem.Instance.DialogueData.English = false;
            DialogueSystem.Instance.DialogueData.Spanish = false;
        }

        DialogueSystem.Instance.ChangeLanguage();
    }

    #endregion

    #region Pause Control
    private void PauseControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pause)
            {
                Pause = false;
                UIManager.Instance.SettingsCanvas.SetActive(false);
                UIManager.Instance.MainMenuCanvas.SetActive(false);
            }
            else
            {
                Pause = true;
                UIManager.Instance.MainMenuCanvas.SetActive(true);
            }
        }

        if (Pause)
        {
            if (!UIManager.Instance.ContinueGameButton.activeInHierarchy) 
                UIManager.Instance.ContinueGameButton.SetActive(true);
            if (UIManager.Instance.StartGameButton.activeInHierarchy) 
                UIManager.Instance.StartGameButton.SetActive(false);

            _playerData.mouseSensitivityX = 0;
            _playerData.mouseSensitivityY = 0;
        }
        else
        {
            _playerData.mouseSensitivityX = _mouseSensitivity;
            _playerData.mouseSensitivityY = _mouseSensitivity;
        }
    }

    public void CursorControl()
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
        SoundControl();

        if (InGame)
        {
            PauseControl();
            CursorControl();
        }
        else
        {
           if(UIManager.Instance.ContinueGameButton.activeInHierarchy) UIManager.Instance.ContinueGameButton.SetActive(false);
           if(!UIManager.Instance.StartGameButton.activeInHierarchy) UIManager.Instance.StartGameButton.SetActive(true);
        }
    }


    private void SoundControl()
    {
        if(_soundData == null) _soundData = SoundManager.Instance.SoundData;

        if (_mainMusicSource == null)
        {
            _mainMusicSource = SoundManager.Instance.CreateModifiableAudioSource(_soundData.MainMenuMusicClip, gameObject, _soundData.MainMenuMusicClipVolume);
            _mainMusicSource.loop = true;
            _mainMusicSource.spatialBlend = 0;
            _mainMusicSource.Play();
        }

        if(InGame)
        {
            if(Pause)
            {
                _mainMusicSource.volume = Mathf.Lerp(_mainMusicSource.volume, _soundData.MainMenuMusicClipVolume, Time.deltaTime * 3f);
            }
            else
            {
                if(SceneManager.GetActiveScene().name == "Level_House")
                {
                    if (_mainMusicSource.volume > 0)
                        _mainMusicSource.volume -= Time.deltaTime * 0.5f;
                }
                _mainMusicSource.volume = Mathf.Lerp(_mainMusicSource.volume, 0, Time.deltaTime * 2f);//_soundData.MainMenuMusicClipVolume / 4, Time.deltaTime * 2f);
            }
        }
    }
}