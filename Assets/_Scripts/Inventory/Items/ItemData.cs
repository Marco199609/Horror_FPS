using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour, IInteractable
{
    public Item Item;

    public void ItemBehaviour()
    {
        GetComponent<IItemBehaviour>().Behaviour(); //Gets item behaviour if clicked on inventory
    }

    public void Interact() //Item behaviour if picked up
    {
        ObjectManager.Instance.InventoryController.AddItem(this);

        //Disables components instead of destroying the object, so that the item behaviour script works in the inventory slot
        if (GetComponent<MeshRenderer>() != null) GetComponent<MeshRenderer>().enabled = false;
        if (GetComponent<Collider>() != false) GetComponent<Collider>().enabled = false;

        //Destroys item children, if any
        int childs = transform.childCount;
        for (int i = 0; i < childs; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public string Description() //Returns description to other scripts
    {
        return Item.description;
    }


    private void OnBecameVisible()
    {
        ObjectManager.Instance.PlayerController.ItemsVisible.Add(this.gameObject); //Must contain a mesh renderer to work
    }

    private void OnBecameInvisible()
    {
        ObjectManager.Instance.PlayerController.ItemsVisible.Remove(this.gameObject); //Must contain a mesh renderer to work
    }


}
