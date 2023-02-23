using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Combinable : MonoBehaviour, IInteractable
{
    public string ActionDescription()
    {
        return "Pickup";
    }

    public void Behaviour()
    {
        PlayerData playerData = FindObjectOfType<PlayerData>();
        playerData.Combinables.Add(gameObject);
        gameObject.transform.SetParent(playerData.camHolder);
        transform.localPosition = new Vector3(0.0586000159f, -0.0859998465f, 0.416999876f);
        transform.localRotation = Quaternion.Euler(new Vector3(25.6294994f, 154.336105f, 106.76442f));
        //gameObject.SetActive(false);
    }

    public void Interact()
    {
        Behaviour();
    }

    public string InteractableDescription()
    {
        return "Key";
    }
}
