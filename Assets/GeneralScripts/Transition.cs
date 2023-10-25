using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Transition : MonoBehaviour
{
    public void LoadLevels()
    {
        SceneManager.LoadScene("LevelSelection");
    }
    
    public void LoadLevel1()
    {
        GlobalVariables.curLevel = "level 1";
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        GlobalVariables.curLevel = "level 2";
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("Level2");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}