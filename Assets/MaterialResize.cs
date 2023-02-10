using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialResize : MonoBehaviour
{
    public Vector2 textureSize;

    void Start() 
    {
        GetComponent<Renderer>().materials[0].mainTextureScale = new Vector2(transform.localScale.x / textureSize.x, transform.localScale.y / textureSize.y);
    }
}
