using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneState : MonoBehaviour
{
    [TextArea]
    [SerializeField] private string _doorStateNotes;
    public DoorState[] DoorStates;

    [TextArea]
    [SerializeField] private string _triggerActivationNotes;
    public bool[] DreamTriggerCollidersActivate, HouseTriggerCollidersActivate;
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


//Main Doors
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
//52: Hallways_Trigger Man Walk
//53: Kitchen door_Jumpscare

//House Triggers
//100: Room_Trigger dialogue 1
//101: Bed_Load dream level
//102: Hallway_Trigger Dialogue 2
//103: Trigger Demo End