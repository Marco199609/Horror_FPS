using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Energy : MonoBehaviour, IItemBehaviour
{
    [SerializeField] private float _energy;
    public void Behaviour()
    {
        ObjectManager.Instance.PlayerFlashlight.AddEnergy(_energy);
        Destroy(gameObject);
    }
}
