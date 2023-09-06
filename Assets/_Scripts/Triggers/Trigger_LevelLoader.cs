using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneState))]
public class Trigger_LevelLoader : MonoBehaviour, ITrigger
{
    private enum Level {House, Dream};
    [SerializeField] private Level _loadLevel;

    [SerializeField] private Vector3 _playerSpawnPosition; //Leave y as 0
    [SerializeField] private float _playerSpawnRotation; //Player y rotation
    [SerializeField] private bool _setLevelMaskInstantly;

    public void TriggerBehaviour(float triggerDelay)
    {
        StartCoroutine(Trigger(triggerDelay));
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(triggerDelay);
        switch (_loadLevel)
        {
            case Level.House:
                {
                    LevelLoader.Instance.LoadHouseLevel(new Vector3(_playerSpawnPosition.x,
                        0, _playerSpawnPosition.z), _playerSpawnRotation, _setLevelMaskInstantly, gameObject.GetComponent<SceneState>());
                    break;
                }
            case Level.Dream:
                {
                    LevelLoader.Instance.LoadDreamLevel(new Vector3(_playerSpawnPosition.x,
                        0, _playerSpawnPosition.z), _playerSpawnRotation, _setLevelMaskInstantly, gameObject.GetComponent<SceneState>());
                    break;
                }
        }
    }
}
