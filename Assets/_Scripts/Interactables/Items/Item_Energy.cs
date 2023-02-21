using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Energy : MonoBehaviour, IInteractable
{
    [SerializeField] private string _description;
    [SerializeField] private float _energy;

    public string Description()
    {
        return _description;
    }
    public void Interact()
    {
        Behaviour();
    }

    public void Behaviour()
    {
        ObjectManager.Instance.PlayerFlashlight.AddEnergy(_energy);
        Destroy(gameObject);
    }
}
