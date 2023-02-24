using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Clock : MonoBehaviour, IInteractable
{
    public Transform HourHand, MinuteHand;

    public string ActionDescription()
    {
        return "Take clock";
    }

    public void Behaviour()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();

        playerController.Inventory.Add(gameObject);
        playerController.GetComponent<PlayerInventory>().Index = playerController.Inventory.Count - 1;

        gameObject.transform.SetParent(FindObjectOfType<PlayerData>().camTransform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void Interact()
    {
        Behaviour();
    }

    public string InteractableDescription()
    {
        return "Clock";
    }


}
