using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InventoryUI))]
public class InventoryController : MonoBehaviour
{
    private ObjectManager objectManager;
    private InventoryInput inventoryInput;
    private GameController gameController;

    private InventoryUI inventoryUI;

    public bool IsInventoryEnabled;
    public int InventorySpace = 12;
    public List<Item> items = new List<Item>();

    private List<InventorySlot> _inventorySlots = new List<InventorySlot>();

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

        _inventorySlots = gameController.inventorySlots;
    }

    private void Update()
    {
        OpenInventoryUI();
    }


    public void Add(Item _item)
    {
        if (items.Count < InventorySpace)
        {
            items.Add(_item);

            for(int i = 0; i < items.Count; i++)
            {
                //print(gameController.inventorySlots[i].SlotIcon.gameObject.name);
                gameController.inventorySlots[i].SlotIcon.GetComponent<Image>().sprite = items[i].icon;
            }
        }
        else
            print("Inventory Full");
    }

    public void Remove(Item _item)
    {
        items.Remove(_item);
    }

    private void OpenInventoryUI()
    {
        inventoryUI.ShowInventoryUI(gameController, inventoryInput, this);
    }
}
