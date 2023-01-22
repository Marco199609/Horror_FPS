using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Health : MonoBehaviour, IItemBehaviour
{
    [SerializeField] private int _health;
    public void Behaviour()
    {
        ObjectManager.Instance.Player.GetComponent<Health>().ModifyHealth(_health);
    }
}
