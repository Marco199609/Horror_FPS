using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform _followObject;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed;
    private void Update()
    {
        //sets current object position to followed object position
        transform.position = Vector3.Lerp(transform.position, new Vector3(_followObject.transform.position.x + offset.x,
                _followObject.transform.position.y + offset.y, _followObject.transform.position.z + offset.z),
                followSpeed * Time.deltaTime);
    }
}
