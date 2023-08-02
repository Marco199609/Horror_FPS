using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadHouseLevel()
    {
        SceneManager.LoadScene("Level_House");
    }

    public void LoadDreamLevel()
    {
        SceneManager.LoadScene("Level_Dream");
    }
}
