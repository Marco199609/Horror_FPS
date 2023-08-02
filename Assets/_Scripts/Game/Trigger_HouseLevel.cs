using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger_HouseLevel : MonoBehaviour
{
    [SerializeField] private GameObject Level0;
    [SerializeField] private GameObject _player, _playerLookingLight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _playerLookingLight.SetActive(false);
            SceneManager.LoadScene("Level_House");
          //  Level0.SetActive(false);
        }
    }
}
