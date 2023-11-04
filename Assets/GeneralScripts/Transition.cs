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
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level1");
    }

    public void LoadLevel2()
    {
        GlobalVariables.curLevel = "level 2";
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level2");
    }
    public void LoadLevel3()
    {
        GlobalVariables.curLevel = "level 3";
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level1_Gun_Amy");
    }

    public void LoadLevel3()
    {
        GlobalVariables.curLevel = "level 3";
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level3");
    }

    public void LoadTutorial1()
    {
        GlobalVariables.curLevel = "tutorial 1";
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Tutorial1");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("_MainMenu");
    }

    public void LoadGameOverLost()
    {
        SceneManager.LoadScene("_Game Over_Lost");
    }

    public void LoadGameOverWon()
    {
        SceneManager.LoadScene("_Game Over_Won");
    }

    public void ReloadPreviosLevel()
    {
        switch (GlobalVariables.curLevel)
        {
            case "level 1":
                LoadLevel1();
                break;
            case "level 2":
                LoadLevel2();
                break;
        }
    }
}