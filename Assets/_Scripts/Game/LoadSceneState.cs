using UnityEngine;



public class LoadSceneState : MonoBehaviour
{
    

    [Header("Door states in index order")]
    public DoorState[] DoorStates;

    [SerializeField] private GameObject[] _dreamLevelTriggers;  //To be used only in main scene loader
    [SerializeField] private bool[] _activateDreamLevelTriggers; //To be passed from each level trigger

    [SerializeField] private GameObject[] _houseLevelTriggers; //To be used only in main scene loader
    [SerializeField] private bool[] _activateHouseLevelTriggers; //To be passed from each level trigger

    private GameObject[] Doors;

    public LoadSceneState GetSceneState(LoadSceneState sceneStateLoader)
    {
        sceneStateLoader.DoorStates = DoorStates;
        sceneStateLoader._activateDreamLevelTriggers = _activateDreamLevelTriggers;
        sceneStateLoader._activateHouseLevelTriggers = _activateHouseLevelTriggers;
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

        if (_dreamLevelTriggers.Length == _activateDreamLevelTriggers.Length)
        {
            for(int i = 0; i < _dreamLevelTriggers.Length; i++)
            {
                if (_activateDreamLevelTriggers[i])
                    _dreamLevelTriggers[i].SetActive(true);
                else
                    _dreamLevelTriggers[i].SetActive(false);
            }
        }
        else
            print("Dream level triggers and activation bools in scene loader do not match!");

        if (_houseLevelTriggers.Length == _activateHouseLevelTriggers.Length)
        {
            for (int i = 0; i < _houseLevelTriggers.Length; i++)
            {
                if (_activateHouseLevelTriggers[i])
                    _houseLevelTriggers[i].SetActive(true);
                else
                    _houseLevelTriggers[i].SetActive(false);
            }
        }
        else
            print("House level triggers and activation bools in scene loader do not match!");
    }
}
