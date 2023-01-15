using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public int InventorySpace = 10;
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        //Adds this object to object manager for future use
        ObjectManager.Instance.InventoryController = this;
    }

    public void Add(Item _item)
    {
        if (items.Count < InventorySpace)
            items.Add(_item);
        else
            print("Inventory Full");
    }

    public void Remove(Item _item)
    {
        items.Remove(_item);
    }
}
