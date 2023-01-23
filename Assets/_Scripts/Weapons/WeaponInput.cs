using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    public bool LeftMouseDownInput { get; private set; }
    public bool shootInput { get; private set; }
    public bool leftMouseUpInput { get; private set; }
    public bool reloadInput { get; private set; }
    public bool aimInput { get; private set; }
    public bool WeaponChangeInput { get; private set; }
    public float mouseScrollInput { get; private set;}
    public int weaponIndexInput { get; private set; }

    private void Awake()
    {
        ObjectManager.Instance.WeaponInput = this;
    }

    private void Update()
    {
        LeftMouseDownInput = Input.GetMouseButtonDown(0);
        shootInput = Input.GetMouseButton(0);
        leftMouseUpInput = Input.GetMouseButtonUp(0);
        reloadInput = Input.GetKey(KeyCode.R);
        aimInput = Input.GetMouseButton(1);
        mouseScrollInput = Input.GetAxisRaw("Mouse ScrollWheel");

        //Gets key as an int and passes it for weapon change
        if (Input.GetKeyDown(KeyCode.Alpha1)
            || Input.GetKeyDown(KeyCode.Alpha2)
            || Input.GetKeyDown(KeyCode.Alpha3))
        {
            int keyValue;
            int.TryParse(Input.inputString, out keyValue);
            weaponIndexInput = keyValue - 1; //passes the key value minus one, to match the weapon array index
            WeaponChangeInput = true;
        }
        else
        {
            weaponIndexInput = -1;
            WeaponChangeInput = false;
        }
    }
}
