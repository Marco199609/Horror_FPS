using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Health : MonoBehaviour, IInteractable
{
    [SerializeField] private string _description;
    [SerializeField] private int _health;


    public string InteractableDescription()
    {
        return _description;
    }

    public string ActionDescription()
    {
        return "Take";
    }

    public void Interact()
    {
        Behaviour();
    }
    public void Behaviour()
    {
        ObjectManager.Instance.Player.GetComponent<Health>().ModifyHealth(_health);
    }
}
