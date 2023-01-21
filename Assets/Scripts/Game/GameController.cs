using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private int _targetFramerate, _vSyncCount;
    [SerializeField] private Text fpsText;
    [SerializeField] private float hudRefreshRate = 1f;
    [SerializeField] private bool _showFramerate;
    private float _timer;

    [Header("Inventory Items")]
    public GameObject InventoryPanel;
    private InventoryController inventoryController;
    public List<InventoryItemSlot> inventoryItemSlots = new List<InventoryItemSlot>();
    public List<InventoryWeaponSlot> inventoryWeaponSlots = new List<InventoryWeaponSlot>();

    [Header("UI Items")]
    public TextMeshProUGUI ItemOrWeaponDescription;

    private void Awake()
    {
        //Adds this object to object manager for future use
        ObjectManager.Instance.GameController = this;
        
        Application.targetFrameRate = _targetFramerate;
        QualitySettings.vSyncCount = _vSyncCount;
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
        if (Time.unscaledTime > _timer && _showFramerate)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = "FPS: " + fps;
            _timer = Time.unscaledTime + hudRefreshRate;
        }
        else if(!_showFramerate)
        {
            if(fpsText.gameObject.activeInHierarchy) fpsText.gameObject.SetActive(false);
        }
    }


    public void RemoveItemFromInventory()
    {
        //passes inventory slot clicked to the inventory controller 
        ObjectManager.Instance.InventoryController.RemoveItem(EventSystem.current.currentSelectedGameObject.GetComponentInParent<InventoryItemSlot>());
    }

    public void RemoveWeaponFromInventory()
    {
        //passes inventory slot clicked to the inventory controller 
        ObjectManager.Instance.InventoryController.RemoveWeapon(EventSystem.current.currentSelectedGameObject.GetComponentInParent<InventoryWeaponSlot>());
    }
}
