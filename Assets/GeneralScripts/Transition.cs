using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Transition : MonoBehaviour
{
    public void LoadLevels()
    {
        SceneManager.LoadScene("_LevelSelection");
    }

    public void LoadLevel1()
    {
        GlobalVariables.curLevel = "level 1";
        SceneManager.LoadScene("_Level1");
    }

    public void LoadLevel2()
    {
        GlobalVariables.curLevel = "level 2";
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level2");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("_MainMenu");
    }
}