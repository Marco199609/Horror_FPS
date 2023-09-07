using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Character_Zombie : MonoBehaviour
{
    [SerializeField] private Animator _zombieAnimator;
    [SerializeField] private GameObject _player, _zombieRoot;

    private bool _turnedAround;
    private void OnEnable()
    {
        
    }

    void Update()
    {
        if (_player == null)
            _player = PlayerController.Instance.Player;

        Vector3 targetPostition = new Vector3(_player.transform.position.x, _zombieRoot.transform.position.y, _player.transform.position.z);

        _zombieRoot.transform.LookAt(targetPostition);

        if (!_turnedAround)
        {
            _zombieAnimator.SetBool("Turn Around", true);
            _turnedAround = true;
        }
    }
}
