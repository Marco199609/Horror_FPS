using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LoadSceneState))]
public class Trigger_LevelLoader : MonoBehaviour
{
    private enum Level {House, Dream};
    [SerializeField] private Level _loadLevel;

    [SerializeField] private Vector3 _playerSpawnPosition; //Leave y as 0
    [SerializeField] private float _playerSpawnRotation; //Player y rotation
    [SerializeField] private bool _setLevelMaskInstantly;

    public void LoadLevel()
    {
        switch (_loadLevel)
        {
            case Level.House:
                {
                    LevelLoader.Instance.LoadHouseLevel(new Vector3(_playerSpawnPosition.x, 
                        0, _playerSpawnPosition.z), _playerSpawnRotation, _setLevelMaskInstantly, gameObject.GetComponent<LoadSceneState>());
                    break;
                }
            case Level.Dream:
                {
                    LevelLoader.Instance.LoadDreamLevel(new Vector3(_playerSpawnPosition.x,
                        0, _playerSpawnPosition.z), _playerSpawnRotation, _setLevelMaskInstantly, gameObject.GetComponent<LoadSceneState>());
                    break;
                }
        }
    }
}
