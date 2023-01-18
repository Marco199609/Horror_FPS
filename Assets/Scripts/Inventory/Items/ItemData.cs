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
}
