using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_LevelChange : MonoBehaviour
{
    private enum Level {House, Dream};
    [SerializeField] private Level _loadLevel;

    [SerializeField] private Vector3 _playerSpawnPosition; //Leave y as 0, z is y rotation in Unity
    [SerializeField] private float _playerSpawnRotation; //Player y rotation

    public void LoadLevel(PlayerController playerController)
    {
        switch (_loadLevel)
        {
            case Level.House:
                {
                    LevelManager.Instance.LoadHouseLevel(playerController, new Vector3(_playerSpawnPosition.x, 
                        playerController.Player.transform.localPosition.y, _playerSpawnPosition.z), _playerSpawnRotation);
                    break;
                }
            case Level.Dream:
                {
                    LevelManager.Instance.LoadDreamLevel(playerController, new Vector3(_playerSpawnPosition.x,
                        playerController.Player.transform.localPosition.y, _playerSpawnPosition.z), _playerSpawnRotation);
                    break;
                }
        }

        //_levelManager.LoadHouseLevel(_playerController, new Vector3(11.2f,
        //_playerController.Player.transform.localPosition.y, 19.6f), 175f); //Sets player in bedroom
    }
}
