using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_camera == null) _camera = Camera.main;

        Vector3 targetPostition = new Vector3(_camera.transform.position.x, _camera.transform.position.y, _camera.transform.position.z);

        transform.LookAt(targetPostition);
    }
}
