using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    public bool OpenInventory { get; private set; }
    public bool Slot1Input, Slot2Input, Slot3Input;

    private void Awake()
    {
        //Adds this object to object manager for future use
        ObjectManager.Instance.InventoryInput = this;
    }

    private void Update()
    {
        OpenInventory = Input.GetKeyDown(KeyCode.Q);

        Slot1Input = Input.GetKeyDown(KeyCode.Alpha4);
        Slot2Input = Input.GetKeyDown(KeyCode.Alpha5);
        Slot3Input = Input.GetKeyDown(KeyCode.Alpha6);
    }
}
