using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Transition : MonoBehaviour
{
    public void LoadLevels()
    {
        SceneManager.LoadScene("_LevelSelection");
    }

    public void LoadLevel1_1()
    {
        GlobalVariables.curLevel = "_Level1-1";
        // GlobalVariables.leftBound = -30;
        // GlobalVariables.rightBound = 70;
        GlobalVariables.gravityLevel = false;
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level1-1");
    }

    public void LoadLevel1_2()
    {
        GlobalVariables.curLevel = "_Level1-2";
        GlobalVariables.gravityLevel = false;
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level1-2");
    }
    public void LoadLevel2_1()
    {
        GlobalVariables.curLevel = "level 2";
        GlobalVariables.gravityLevel = false;
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level2-1");
    }
    public void LoadLevel2_2()
    {
        GlobalVariables.curLevel = "level 2";
        GlobalVariables.gravityLevel = false;
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level2-2");
    }
    public void LoadLevel3_1()
    {
        GlobalVariables.curLevel = "level 2";
        GlobalVariables.gravityLevel = false;
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level3-1");
    }
    public void LoadLevel3_2()
    {
        GlobalVariables.curLevel = "level 2";
        GlobalVariables.gravityLevel = false;
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level3-2");
    }
    public void LoadLevel4_1()
    {
        GlobalVariables.curLevel = "level4-1";
        GlobalVariables.gravityLevel = true;
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level4-1");
    }
    public void LoadLevel4_2()
    {
        GlobalVariables.curLevel = "level 2";
        GlobalVariables.gravityLevel = false;
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level4-2");
    }
    public void LoadLevel5_1()
    {
        GlobalVariables.curLevel = "level 2";
        GlobalVariables.gravityLevel = false;
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level5-1");
    }
    public void LoadLevel5_2()
    {
        GlobalVariables.curLevel = "level 2";
        GlobalVariables.gravityLevel = false;
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level5-2");
    }
    // public void LoadLevel3()
    // {
    //     GlobalVariables.curLevel = "level 3";
    //     GlobalVariables.platformMap = new Dictionary<string, int>();
    //     SceneManager.LoadScene("_Level1_Gun_Amy");
    // }

    public void LoadLevel3()
    {
        GlobalVariables.curLevel = "level 3";
        GlobalVariables.gravityLevel = false;
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Level3");
    }

    public void LoadTutorial1()
    {
        GlobalVariables.curLevel = "tutorial 1";
        GlobalVariables.gravityLevel = false;
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Tutorial1");
    }

    public void LoadTutorial2()
    {
        GlobalVariables.curLevel = "tutorial 2";
        GlobalVariables.gravityLevel = false;
        GlobalVariables.platformMap = new Dictionary<string, int>();
        SceneManager.LoadScene("_Tutorial2");
    }

    public void LoadTutorial3()
    {
            GlobalVariables.curLevel = "tutorial 3";
            GlobalVariables.gravityLevel = false;
            GlobalVariables.platformMap = new Dictionary<string, int>();
            SceneManager.LoadScene("_Tutorial3");
    }

    public void LoadTutorial4()
    {
            GlobalVariables.curLevel = "tutorial 4";
            GlobalVariables.gravityLevel = false;
            GlobalVariables.platformMap = new Dictionary<string, int>();
            SceneManager.LoadScene("_Tutorial4");
    }

    public void LoadTutorial5()
    {
            GlobalVariables.curLevel = "tutorial 5";
            GlobalVariables.gravityLevel = false;
            GlobalVariables.platformMap = new Dictionary<string, int>();
            SceneManager.LoadScene("_Tutorial5");
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
            case "_Level1-1":
                LoadLevel1_1();
                break;
            case "_Level1-2":
                LoadLevel1_2();
                break;
        }
    }
}