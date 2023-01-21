using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public Item Item;

    public void ItemBehaviour()
    {
        ObjectManager.Instance.Player.GetComponent<Health>().ModifyHealth(10);
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
