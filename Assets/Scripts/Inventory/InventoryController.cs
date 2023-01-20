using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(InventoryUI))]
[RequireComponent(typeof(InventorySlotUpdate))]
public class InventoryController : MonoBehaviour
{
    private ObjectManager objectManager;
    private InventoryInput inventoryInput;
    private GameController gameController;
    private WeaponGeneralData weaponGeneralData;

    //Scripts on this gameobject
    private InventoryUI inventoryUI;
    private InventorySlotUpdate _inventorySlotUpdate;

    public bool IsInventoryEnabled;
    public int InventorySpace;
    public List<ItemData> itemDatas = new List<ItemData>();
    public GameObject[] weapons;

    [SerializeField] private List<InventoryItemSlot> _inventoryItemSlots = new List<InventoryItemSlot>();
    [SerializeField] private List<InventoryWeaponSlot> _inventoryWeaponSlots = new List<InventoryWeaponSlot>();

    private void Awake()
    {
        //Adds this object to object manager for future use
        ObjectManager.Instance.InventoryController = this;
    }

    private void Start()
    {
        objectManager = ObjectManager.Instance;
        inventoryInput = objectManager.InventoryInput;
        gameController = objectManager.GameController;
        weaponGeneralData = objectManager.WeaponGeneralData;

        inventoryUI = GetComponent<InventoryUI>();
        _inventorySlotUpdate = GetComponent<InventorySlotUpdate>();

        _inventoryItemSlots = gameController.inventoryItemSlots;
        _inventoryWeaponSlots = gameController.inventoryWeaponSlots;
        InventorySpace = _inventoryItemSlots.Count;

        weapons = new GameObject[weaponGeneralData.WeaponsAvailable.Length];

        UpdateInventorySlots();
    }

    private void Update()
    {
        OpenInventoryUI();
    }

    //Used in player item pickup
    public void AddItem(ItemData _itemData)
    {
        if (itemDatas.Count < InventorySpace)
        {
            itemDatas.Add(_itemData);
            UpdateInventorySlots();
        }
        else
            print("Inventory Full");
    }

    public void AddWeapon(WeaponData weaponData)
    {
        weapons[weaponData.weaponIndex] = weaponData.gameObject;
        weaponGeneralData.WeaponsAvailable[weaponData.weaponIndex] = weaponData.gameObject;

        UpdateInventorySlots();
    }

    public void RemoveItem(InventoryItemSlot currentItemSlot)
    {
        itemDatas.Remove(currentItemSlot.ItemData);
        UpdateInventorySlots();
    }

    public void RemoveWeapon(InventoryWeaponSlot currentWeaponSlot)
    {
        weapons[currentWeaponSlot.weaponData.weaponIndex] = null;
        UpdateInventorySlots();
    }

    private void UpdateInventorySlots()
    {
        _inventorySlotUpdate.UpdateSlots(this, _inventoryItemSlots, _inventoryWeaponSlots);
    }

    private void OpenInventoryUI()
    {
        inventoryUI.ShowInventoryUI(gameController, inventoryInput, this);
    }
}
