using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer_Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateVector;
    [SerializeField] private Transform _camTransform;
    [SerializeField] private float _speed;
    void Update()
    {
        transform.Rotate(_rotateVector.x * Time.deltaTime, _rotateVector.y * Time.deltaTime, _rotateVector.z * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, _camTransform.position, _speed * Time.deltaTime);
    }
}
