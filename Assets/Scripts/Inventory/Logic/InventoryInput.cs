using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    public bool OpenInventory { get; private set; }

    private void Awake()
    {
        //Adds this object to object manager for future use
        ObjectManager.Instance.InventoryInput = this;
    }

    private void Update()
    {
        OpenInventory = Input.GetKeyDown(KeyCode.Q);
    }
}
