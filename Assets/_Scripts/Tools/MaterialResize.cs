using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MaterialResize : MonoBehaviour
{
    public Vector2 textureSize;
    [SerializeField] private bool _lockZAxis = true; //Prevents z axis to be modified, the material does not scale in this axis
    [SerializeField] private float _zScale = 0.2f;
    private Vector3 _objectScale;

    void Start() 
    {
       
    }

    private void Update()
    {
        if(!Application.isPlaying)
        {
            GetComponent<Renderer>().sharedMaterial.mainTextureScale = new Vector2(transform.localScale.x / textureSize.x, transform.localScale.y / textureSize.y);

            if(_lockZAxis)
            {
                _objectScale = new Vector3(transform.localScale.x, transform.localScale.y, _zScale);
                transform.localScale = _objectScale;
            }
        }
    }

}
