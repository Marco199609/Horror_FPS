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
        GetComponent<IItemBehaviour>().Behaviour();
    }

    public string Description() //Returns description to other scripts
    {
        return Item.description;
    }

    private void OnBecameVisible()
    {
        ObjectManager.Instance.PlayerController.InteractablesInSight.Add(this.gameObject); //Must contain a mesh renderer to work
    }

    private void OnBecameInvisible()
    {
        ObjectManager.Instance.PlayerController.InteractablesInSight.Remove(this.gameObject); //Must contain a mesh renderer to work
    }
}
