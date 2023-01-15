using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    public static void Execute()
    {
        CreateObject<ObjectManager>();

        CreateObject<PlayerController>();
        CreateObject<WeaponController>();
        CreateObject<InventoryController>();

        CreateObject<PlayerInput>();
        CreateObject<WeaponInput>();
        CreateObject<InventoryInput>();
    }

    public static void CreateObject<T>() where T : MonoBehaviour
    {
        if (Object.FindObjectOfType<T>() != null) return;

        var obj = new GameObject(typeof(T).Name);
        obj.AddComponent<T>();
    }
}
