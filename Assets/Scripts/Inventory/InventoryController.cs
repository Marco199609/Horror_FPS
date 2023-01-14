using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectManager.Instance.InventoryController = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
