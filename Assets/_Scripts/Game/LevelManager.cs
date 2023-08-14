using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Image _levelChangeMask;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _playerLookingLight;

    private float _playerFreezeTime = 0.5f, _removeMaskDefaultDelay = 1.5f, _maskSpeed = 1.5f; //Defaults
    private float _playerRotationY, _currentTransparency, _removeMaskDelay;
    private bool _changeScene, _removeMask, _isMaskInstant, _activatePlayerLookingLight;
    private string _sceneName;
    private Vector3 _playerLocalPosition;
    private CinemachinePOV _vCamCinemachinePOV;


    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        _vCamCinemachinePOV = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    private void Update()
    {
        if (_changeScene)
            PrepareSceneChange(_isMaskInstant, _maskSpeed);
        else if (_removeMask)
            StartCoroutine(RemoveLevelChangeMask(_removeMaskDelay, _maskSpeed));
    }

    #region Level Change
    public void LoadHouseLevel(Vector3 playerLocalPosition, float playerRotationY, bool isLevelMaskInstant)
    {
        _sceneName = "Level_House";
        _changeScene = true;
        _isMaskInstant = isLevelMaskInstant;
        _activatePlayerLookingLight = false;
        _playerLocalPosition = playerLocalPosition;
        _playerRotationY = playerRotationY;
    }

    public void LoadDreamLevel(Vector3 playerLocalPosition, float playerRotationY, bool isLevelMaskInstant)
    {
        _sceneName = "Level_Dream";
        _changeScene = true;
        _isMaskInstant = isLevelMaskInstant;
        _activatePlayerLookingLight = true;
        _playerLocalPosition = playerLocalPosition;
        _playerRotationY = playerRotationY;
    }
    #endregion

    private void PrepareSceneChange(bool isLevelMaskInstant, float speed)
    {
        if (isLevelMaskInstant)
        {
            _currentTransparency = 1;
            _removeMaskDelay = _removeMaskDefaultDelay;
        }

        else
        {
            _removeMaskDelay = _removeMaskDefaultDelay / 3;
            _currentTransparency += Time.deltaTime * speed;
        }


        if (_currentTransparency >= 1)
        {
            _currentTransparency = 1;
            _levelChangeMask.color = new Color(0, 0, 0, _currentTransparency); //Prevents light leaks

            _playerLookingLight.SetActive(_activatePlayerLookingLight);
            _playerController.PlayerFlashlight.TurnOff();
            SceneManager.LoadScene(_sceneName);
            StartCoroutine(FreezePlayerMotion(_playerLocalPosition, _playerFreezeTime));
            SetPlayerRotation(_playerRotationY);

            _changeScene = false;
            _removeMask = true;
        }

        _levelChangeMask.color = new Color(0, 0, 0, _currentTransparency);
    }

    #region Player Props
    IEnumerator FreezePlayerMotion(Vector3 playerLocalPosition, float freezeTime)
    {
        _playerController.FreezePlayerMovement = true;
        SetPlayerPosition(playerLocalPosition);

        yield return new WaitForSeconds(freezeTime);

        _playerController.FreezePlayerMovement = false;
    }

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

    private IEnumerator RemoveLevelChangeMask(float delay, float speed)
    {
        yield return new WaitForSeconds(delay);

        if (_currentTransparency > 0)
        {
            _currentTransparency -= Time.deltaTime * speed;
        }
        else if (_currentTransparency <= 0)
        {
            _currentTransparency = 0;
            _removeMask = false;
        }

        _levelChangeMask.color = new Color(0, 0, 0, _currentTransparency);
    }
}