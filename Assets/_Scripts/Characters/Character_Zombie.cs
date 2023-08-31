using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Character_Zombie : MonoBehaviour
{
    [SerializeField] private float _moveSpeed, _playerDistance;
    private bool _moveTowardsPlayer;
    private GameObject _player;

    private void OnEnable()
    {
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (_player == null)
            _player = PlayerController.Instance.Player;
        else
        {
            Vector3 targetPostition = new Vector3(_player.transform.position.x,
                               this.transform.position.y,
                               _player.transform.position.z);

            transform.LookAt(targetPostition);
            if(_moveTowardsPlayer)
                transform.position += transform.forward * _moveSpeed * Time.deltaTime;

            if((_player.transform.position - transform.position).magnitude <= _playerDistance)
            {
                _moveTowardsPlayer = true;
            }

            
        }
    }
}
