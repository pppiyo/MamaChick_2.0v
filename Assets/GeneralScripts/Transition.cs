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
    
    public void LoadPortalLevel1()
    {
        // GlobalVariables.curLevel = "level 1";
        SceneManager.LoadScene("Tutorial_Portal_Geng");
    }
    
    public void LoadPortalLevel2()
    {
        // GlobalVariables.curLevel = "level 1";
        SceneManager.LoadScene("Level1_Portal_Geng 1");
    }
    
    public void LoadMonsterLevel1()
    {
        // GlobalVariables.curLevel = "level 1";
        SceneManager.LoadScene("Level1_Monster_Chris");
    }
    
    public void LoadMonsterLevel2()
    {
        // GlobalVariables.curLevel = "level 1";
        SceneManager.LoadScene("Level2_Monster_Chris");
    }

    public void LoadGravityLevel1()
    {
        // GlobalVariables.curLevel = "level 1";
        SceneManager.LoadScene("Level_Mei");
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}