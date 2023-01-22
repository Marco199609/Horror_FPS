using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpRotation : MonoBehaviour
{
    [SerializeField] Transform _objectLerpX, _objectLerpY, _objectLerpZ;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float rotationSpeed;
    private Vector3 rot;
    private void Update()
    {
        //gets rotation
        rot.x = _objectLerpX.rotation.eulerAngles.x + offset.x;
        rot.y = _objectLerpY.rotation.eulerAngles.y + offset.y;
        rot.z = _objectLerpZ.rotation.eulerAngles.z + offset.z;

        //Rotates object
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rot),
                rotationSpeed * Time.deltaTime);
    }
}
