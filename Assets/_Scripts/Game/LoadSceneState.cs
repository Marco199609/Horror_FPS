using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LoadSceneState : MonoBehaviour
{
    private GameObject[] Doors;

    [Header("Door states in index order")]
    public DoorState[] DoorStates;

    public LoadSceneState GetSceneState(LoadSceneState sceneStateLoader)
    {
        sceneStateLoader.Doors = Doors;
        sceneStateLoader.DoorStates = DoorStates;
        return sceneStateLoader;
    }

    public void ApplySceneState()
    {
        if(DoorStates.Length > 0)
        {
            Doors = GameObject.FindGameObjectsWithTag("Door");

            for (int i = 0; i < Doors.Length; i++)
            {
                Prop_Door door = Doors[i].GetComponent<Prop_Door>();

                door._currentDoorState = DoorStates[door.DoorIndex];
            }
        }
    }
}
