using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer_ScaleUP : MonoBehaviour
{
    [SerializeField] private float _speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(1 * Time.deltaTime * _speed, 1 * Time.deltaTime * _speed, 1 * Time.deltaTime * _speed);
    }
}
