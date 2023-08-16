using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateLoader : MonoBehaviour
{
    public Dictionary<int, GameObject> objects = new Dictionary<int, GameObject>();

    private DoorState[] DoorStates;
    private bool[] DreamTriggerCollidersActivate, HouseTriggerCollidersActivate;

    public static SceneStateLoader Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void GetSceneState(SceneState state)
    {
        DoorStates = state.DoorStates;
        DreamTriggerCollidersActivate = state.DreamTriggerCollidersActivate;
        HouseTriggerCollidersActivate= state.HouseTriggerCollidersActivate;
    }

    public void LoadSceneState()
    {
        for(int i = 1; i < 999; i++) //0 is default ID, not used
        {
            if (i >= 1 && i <= 14) //Set main door states
            {
                if (!objects.ContainsKey(i))
                    print("door not found in index " +  i + "!");
                else
                    objects[i].GetComponent<Prop_Door>()._currentDoorState = DoorStates[i - 1]; //Sets door states in order from 0 to 13
            }

            if(i >= 50 &&  i <= 99) //Activates dream level triggers
            {
                if (objects.ContainsKey(i))
                {
                    objects[i].SetActive(DreamTriggerCollidersActivate[i - 50]);
                }
            }

            if (i >= 100 && i <= 149) //Activates house level triggers
            {
                if (objects.ContainsKey(i))
                {
                    objects[i].SetActive(HouseTriggerCollidersActivate[i - 100]);
                }
            }
        }
    }
}

//ID Dictionary (IDs must go in order, from 0 to 999
//0: Default ID, not used
//1-14: Main doors in dream level
//15-34: Reserved for drawers and secondary doors on dream level
//35-49: Reserved for dream door keys
//50-99: Triggers in dream level
//100-149: Triggers in house level
//150-249: Objects in dream level
//250-349: Objects in house level


//Main Doors (in SceneState order, starting from 0)
//0: Director's office
//1: Infirmary
//2: Storage
//3: Men's Bathroom
//4: Women's Bathroom
//5: 4 year olds' room
//6: 5 year olds' room
//7: Library
//8: Canteen
//9: Kitchen
//10: 3 year olds' room
//11: 2 year olds' room
//12: Teachers' room
//13: Secretary's office



//Dream Triggers
//50: Hall_Trigger hall light
//51: Teachers room_Trigger Director's, 5 year olds, hall light off
//52: Bathroom_Trigger all hall lights on
//53: Kitchen door_Jumpscare

//House Triggers
//100: Room_Trigger dialogue 1
//101: Bed_Load dream level
//102: Hallway_Trigger Dialogue 2