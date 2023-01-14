using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    public bool shootInput { get; private set; }
    public bool reloadInput { get; private set; }
    public bool aimInput { get; private set; }
    public bool leftMouseUpInput { get; private set; }
    public float mouseScrollInput { get; private set;}
    public int weaponIndexInput { get; private set; }

    private void Awake()
    {
        ObjectManager.Instance.WeaponInput = this;
    }

    private void Update()
    {
        shootInput = Input.GetMouseButton(0);
        reloadInput = Input.GetKeyDown(KeyCode.R);
        aimInput = Input.GetMouseButton(1);
        leftMouseUpInput = Input.GetMouseButtonUp(0);
        mouseScrollInput = Input.GetAxisRaw("Mouse ScrollWheel");

        //Gets key as an int and passes it for weapon change
        if (Input.GetKeyDown(KeyCode.Alpha1)
            || Input.GetKeyDown(KeyCode.Alpha2)
            || Input.GetKeyDown(KeyCode.Alpha3)
            || Input.GetKeyDown(KeyCode.Alpha4)
            || Input.GetKeyDown(KeyCode.Alpha5)
            || Input.GetKeyDown(KeyCode.Alpha6)
            || Input.GetKeyDown(KeyCode.Alpha7)
            || Input.GetKeyDown(KeyCode.Alpha8)
            || Input.GetKeyDown(KeyCode.Alpha9))
        {
            int keyValue;
            int.TryParse(Input.inputString, out keyValue);
            weaponIndexInput = keyValue - 1; //passes the key value minus one, to match the weapon array index
        }
        else
            weaponIndexInput = -1;
    }
}
