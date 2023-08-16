using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(SceneStateLoader))]
public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Image _levelChangeMask;
    [SerializeField] private GameObject _playerLookingLight;

    private float _removeMaskDefaultDelay = 1.5f, _maskSpeed = 1.5f; //Defaults
    private float _playerRotationY, _currentTransparency, _removeMaskDelay;
    private bool _changeScene, _removeMask, _isMaskInstant, _activatePlayerLookingLight, _sceneStateLoaded;
    private string _sceneName;
    private Vector3 _playerLocalPosition;
    private CinemachinePOV _vCamCinemachinePOV;
    private SceneStateLoader _sceneStateLoader;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        _vCamCinemachinePOV = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        _sceneStateLoader = gameObject.GetComponent<SceneStateLoader>();
    }

    private void Update()
    {
        if (_changeScene)
            PrepareSceneChange();
        else if (_removeMask)
            StartCoroutine(RemoveLevelChangeMask(_removeMaskDelay));     
    }

    #region Level Change
    public void LoadHouseLevel(Vector3 playerLocalPosition, float playerRotationY, bool isLevelMaskInstant, SceneState sceneState)
    {
        _sceneName = "Level_House";
        _changeScene = true;
        _isMaskInstant = isLevelMaskInstant;
        _activatePlayerLookingLight = false;
        _playerLocalPosition = playerLocalPosition;
        _playerRotationY = playerRotationY;

        _sceneStateLoader.GetSceneState(sceneState);
        _sceneStateLoaded = false;
    }

    public void LoadDreamLevel(Vector3 playerLocalPosition, float playerRotationY, bool isLevelMaskInstant, SceneState sceneState)
    {
        _sceneName = "Level_Dream";
        _changeScene = true;
        _isMaskInstant = isLevelMaskInstant;
        _activatePlayerLookingLight = true;
        _playerLocalPosition = playerLocalPosition;
        _playerRotationY = playerRotationY;

        _sceneStateLoader.GetSceneState(sceneState);
        _sceneStateLoaded = false;
    }
    #endregion

    private void PrepareSceneChange()
    {
        if(_isMaskInstant)
        {
            _currentTransparency = 1;
            _removeMaskDelay = _removeMaskDefaultDelay;
        }
        else
        {
            _removeMaskDelay = _removeMaskDefaultDelay / 3;
            _currentTransparency += Time.deltaTime * _maskSpeed;
        }

        if (_currentTransparency >= 1)
        {
            _currentTransparency = 1;
            _levelChangeMask.color = new Color(0, 0, 0, _currentTransparency); //Prevents light leaks

            //Setup player controller
            if (!_playerController.gameObject.activeInHierarchy) _playerController.gameObject.SetActive(true);
            _playerLookingLight.SetActive(_activatePlayerLookingLight);
            _playerController.PlayerFlashlight.TurnOff();
            _playerController.FreezePlayerRotation = true;
            _playerController.FreezePlayerMovement = true;

            SceneManager.LoadScene(_sceneName);
            

            SetPlayerPosition(_playerLocalPosition);
            SetPlayerRotation(_playerRotationY);

            _changeScene = false;
            _removeMask = true;
        }

        _levelChangeMask.color = new Color(0, 0, 0, _currentTransparency);
    }

    #region Player Props

    private void SetPlayerPosition(Vector3 playerLocalPosition)
    {
        _playerController.Player.transform.localPosition = new Vector3(playerLocalPosition.x, 
            _playerController.Player.transform.localPosition.y, playerLocalPosition.z);
    }

    private void SetPlayerRotation(float playerRotationY)
    {
        _playerController.Player.transform.rotation = Quaternion.Euler(new Vector3(0, 
            playerRotationY, 0)); //Easier to rotate player than cinemachine

        _vCamCinemachinePOV.m_HorizontalAxis.Value = 0;
        _vCamCinemachinePOV.m_VerticalAxis.Value = 0;
    }
    #endregion

    private IEnumerator RemoveLevelChangeMask(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_currentTransparency > 0) _currentTransparency -= Time.deltaTime * _maskSpeed;
        else if (_currentTransparency <= 0)
        {
            _currentTransparency = 0;
            _removeMask = false;
            _playerController.FreezePlayerMovement = false;
            _playerController.FreezePlayerRotation = false;

            if(!_sceneStateLoaded)
            {
                _sceneStateLoader.LoadSceneState();
                _sceneStateLoaded = true;
            }
        }

        _levelChangeMask.color = new Color(0, 0, 0, _currentTransparency);
    }
}