using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(InventoryUI))]
[RequireComponent(typeof(InventoryUpdateSlots))]
public class InventoryController : MonoBehaviour
{
    private ObjectManager objectManager;
    private InventoryInput inventoryInput;
    private GameController gameController;

    //Scripts on this gameobject
    private InventoryUI inventoryUI;
    private InventoryUpdateSlots InventoryUpdateSlots;

    public bool IsInventoryEnabled;
    public int InventorySpace;
    public List<ItemData> itemDatas = new List<ItemData>();

    [SerializeField] private List<InventorySlot> _inventorySlots = new List<InventorySlot>();

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

        inventoryUI = GetComponent<InventoryUI>();
        InventoryUpdateSlots = GetComponent<InventoryUpdateSlots>();

        _inventorySlots = gameController.inventorySlots;
        InventorySpace = _inventorySlots.Count;

        UpdateInventorySlots();
    }

    private void Update()
    {
        OpenInventoryUI();
    }

    //Used in player item pickup
    public void Add(ItemData _itemData)
    {
        if (itemDatas.Count < InventorySpace)
        {
            itemDatas.Add(_itemData);
            UpdateInventorySlots();
        }
        else
            print("Inventory Full");
    }

    public void Remove(InventorySlot currentInventorySlot)
    {
        itemDatas.Remove(currentInventorySlot.ItemData);
        UpdateInventorySlots();
    }

    private void UpdateInventorySlots()
    {
        InventoryUpdateSlots.UpdateSlots(_inventorySlots, itemDatas);
    }

    private void OpenInventoryUI()
    {
        inventoryUI.ShowInventoryUI(gameController, inventoryInput, this);
    }
}
