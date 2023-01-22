using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator
{
    private static DontDestroyOnLoad _persistentSystemsParent;
    private static List<GameObject> _objects;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        _persistentSystemsParent = Object.FindObjectOfType<DontDestroyOnLoad>();
        _objects = new List<GameObject>();

        CreateObject<ObjectManager>();

        CreateObject<PlayerController>();
        CreateObject<WeaponController>();
        CreateObject<InventoryController>();

        CreateObject<PlayerInput>();
        CreateObject<WeaponInput>();
        CreateObject<InventoryInput>();

        //Add objects to persistent systems parent
        for(int i = 0; i < _objects.Count; i++)
        {
            _objects[i].transform.SetParent(_persistentSystemsParent.transform);
        }
    }

    public static void CreateObject<T>() where T : MonoBehaviour
    {
        if (Object.FindObjectOfType<T>() != null) return;

        var obj = new GameObject(typeof(T).Name);
        obj.AddComponent<T>();

        _objects.Add(obj);
    }
}
