using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public Item Item;

    public void ItemBehaviour()
    {
        GetComponent<IItemBehaviour>().Behaviour();
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
