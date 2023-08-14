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
    [SerializeField] private GameObject _playerLookingLight;

    private float _playerFreezeTime = 0.5f, _currentTransparency;
    private bool _removeMask;
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
        if(_removeMask)
            RemoveLevelChangeMask(1.5f);
    }

    #region Level Change
    public void LoadHouseLevel(PlayerController playerController, Vector3 playerLocalPosition, float playerRotationY)
    {
        _levelChangeMask.color = Color.black;
        playerController.PlayerFlashlight.TurnOff();
        _playerLookingLight.SetActive(false);
        SceneManager.LoadScene("Level_House");
        StartCoroutine(FreezePlayerMotion(playerController, playerLocalPosition, _playerFreezeTime));
        SetPlayerRotation(playerController, playerRotationY);
        _removeMask = true;
    }

    public void LoadDreamLevel(PlayerController playerController, Vector3 playerLocalPosition, float playerRotationY)
    {
        _levelChangeMask.color = Color.black;
        playerController.PlayerFlashlight.TurnOff();
        _playerLookingLight.SetActive(true);
        SceneManager.LoadScene("Level_Dream");
        StartCoroutine(FreezePlayerMotion(playerController, playerLocalPosition, _playerFreezeTime));
        SetPlayerRotation(playerController, playerRotationY);
        _removeMask = true;
    }
    #endregion

    #region Player Props
    IEnumerator FreezePlayerMotion(PlayerController playerController, Vector3 playerLocalPosition, float freezeTime)
    {
        playerController.FreezePlayerMovement = true;
        SetPlayerPosition(playerController, playerLocalPosition);

        yield return new WaitForSeconds(freezeTime);

        playerController.FreezePlayerMovement = false;
    }

    private void SetPlayerPosition(PlayerController playerController, Vector3 playerLocalPosition)
    {
        playerController.Player.transform.localPosition = new Vector3(playerLocalPosition.x, 
            playerController.Player.transform.localPosition.y, playerLocalPosition.z);
    }

    private void SetPlayerRotation(PlayerController playerController, float playerRotationY)
    {
        playerController.Player.transform.rotation = Quaternion.Euler(new Vector3(0, 
            playerRotationY, 0)); //Easier to rotate player than cinemachine

        _vCamCinemachinePOV.m_HorizontalAxis.Value = 0;
        _vCamCinemachinePOV.m_VerticalAxis.Value = 0;
    }
    #endregion

    private void RemoveLevelChangeMask(float speed)
    {
        if (_levelChangeMask.color == Color.black)
            _currentTransparency = 1;

        _currentTransparency -= Time.deltaTime * speed;
        _levelChangeMask.color = new Color(0, 0, 0, _currentTransparency);
        
        if(_currentTransparency <= 0)
            _removeMask = false;
    }
}
