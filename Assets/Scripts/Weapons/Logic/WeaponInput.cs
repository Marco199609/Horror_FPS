using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    public bool shootInput { get; private set; }
    public bool reloadInput { get; private set; }
    public bool aimInput { get; private set; }
    public bool leftMouseUpInput { get; private set; }


    private void Update()
    {
        shootInput = Input.GetMouseButton(0);
        reloadInput = Input.GetKeyDown(KeyCode.R);
        aimInput = Input.GetMouseButton(1);
        leftMouseUpInput = Input.GetMouseButtonUp(0);
    }
}
