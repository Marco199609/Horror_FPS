using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text fpsText;
    [SerializeField] private float hudRefreshRate = 1f;
    private float _timer;

    [Header("Inventory Items")]
    public GameObject InventoryPanel;
    private InventoryController inventoryController;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    private void Awake()
    {
        //Adds this object to object manager for future use
        ObjectManager.Instance.GameController = this;
    }
    private void Start()
    {
        inventoryController = ObjectManager.Instance.InventoryController;
    }

    // Update is called once per frame
    void Update()
    {
        ShowFPS();
        LockCursor();
    }

    private void LockCursor()
    {
        if(!inventoryController.IsInventoryEnabled)
        {
            //Lock and hide cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            //Unlock and show cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }



    void ShowFPS()
    {
        if (Time.unscaledTime > _timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = "FPS: " + fps;
            _timer = Time.unscaledTime + hudRefreshRate;
        }
    }
}
