using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Man : MonoBehaviour
{
    [SerializeField] private float _speed, _activateDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;

        _activateDuration -= Time.deltaTime;

        if (_activateDuration <= 0 )
            gameObject.SetActive(false);
    }
}
