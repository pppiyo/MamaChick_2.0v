using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Transition : MonoBehaviour
{
    private GameObject DataUploader;
    void Start()
    {
        DataUploader = GameObject.Find("DataUpload");
    }
    public void LoadLevels()
    {
        SceneManager.LoadScene("Nithesh_MainMenu");
    }

    public void LoadLevel1_1()
    {
        globalVariablesReset();
        GlobalVariables.curLevel = "_Level1-1";
        SceneManager.LoadScene("_Level1-1");
    }

    public void LoadLevel1_2()
    {
        globalVariablesReset();
        GlobalVariables.curLevel = "_Level1-2";
        SceneManager.LoadScene("_Level1-2");
    }
    public void LoadLevel2_1()
    {
        globalVariablesReset();
        GlobalVariables.curLevel = "_Level2-1";
        SceneManager.LoadScene("_Level2-1");
    }
    public void LoadLevel2_2()
    {
        globalVariablesReset();
        GlobalVariables.curLevel = "_Level2-2";
        SceneManager.LoadScene("_Level2-2");
    }
    public void LoadLevel3_1()
    {
        globalVariablesReset();
        GlobalVariables.curLevel = "_Level3-1";
        GlobalVariables.gravityLevel = false;
        SceneManager.LoadScene("_Level3-1");
    }
    public void LoadLevel3_2()
    {
        globalVariablesReset();
        GlobalVariables.curLevel = "_Level3-2";
        SceneManager.LoadScene("_Level3-2");
    }
    public void LoadLevel4_1()
    {
        globalVariablesReset();
        GlobalVariables.curLevel = "_Level4-1";
        GlobalVariables.gravityLevel = true;
        SceneManager.LoadScene("_Level4-1");
    }
    public void LoadLevel4_2()
    {
        globalVariablesReset();
        GlobalVariables.curLevel = "_Level4-2";
        GlobalVariables.gravityLevel = true;
        SceneManager.LoadScene("_Level4-2");
    }
    public void LoadLevel5_1()
    {
        globalVariablesReset();
        GlobalVariables.curLevel = "_Level5-1";
        SceneManager.LoadScene("_Level5-1");
    }
    public void LoadLevel5_2()
    {
        globalVariablesReset();
        GlobalVariables.curLevel = "_Level5-2";
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
        globalVariablesReset();
        GlobalVariables.curLevel = "level 3";
        SceneManager.LoadScene("_Level3");
    }

    public void LoadTutorial1()
    {
        globalVariablesReset();
        GlobalVariables.curLevel = "tutorial 1";
        SceneManager.LoadScene("_Tutorial1");
    }

    public void LoadTutorial2()
    {
        globalVariablesReset();
        GlobalVariables.curLevel = "tutorial 2";
        SceneManager.LoadScene("_Tutorial2");
    }

    public void LoadTutorial3()
    {
            globalVariablesReset();
            GlobalVariables.curLevel = "tutorial 3";
            SceneManager.LoadScene("_Tutorial3");
    }

    public void LoadTutorial4()
    {
            globalVariablesReset();
            GlobalVariables.curLevel = "tutorial 4";
            GlobalVariables.gravityLevel = true;
            SceneManager.LoadScene("_Tutorial4");
    }

    public void LoadTutorial5()
    {
            globalVariablesReset();
            GlobalVariables.curLevel = "tutorial 5";
            GlobalVariables.gravityLevel = false;
            SceneManager.LoadScene("_Tutorial5__");
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Nithesh_MainMenu");
    }

    public void LoadGameOverLost()
    {
        GlobalVariables.win = false;
        DataUploader.GetComponent<DataUpload>().PostToDB();
        SceneManager.LoadScene("_Game Over_Lost");
    }

    public void LoadGameOverWon()
    {
        GlobalVariables.win = true;
        DataUploader.GetComponent<DataUpload>().PostToDB();
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
            case "_Level2-1":
                LoadLevel2_1();
                break;
            case "_Level2-2":
                LoadLevel2_2();
                break;
            case "_Level3-1":
                LoadLevel3_1();
                break;
            // case "_Level1-2":
            //     LoadLevel1_2();
            //     break;
            case "_Level4-1":
                LoadLevel4_1();
                break;
            case "_Level4-2":
                LoadLevel4_2();
                break;
        }
    }

    public void NextLevel()
    {
            switch (GlobalVariables.curLevel)
            {
                case "tutorial 1":
                    LoadLevel1_1();
                    break;
                case "tutorial 2":
                    LoadLevel2_1();
                    break;
                case "tutorial 3":
                    LoadLevel3_1();
                    break;
                case "tutorial 4":
                    LoadLevel4_1();
                    break;
                case "tutorial 5":
                    LoadLevels();
                    break;

                case "_Level1-1":
                    LoadLevel1_2();
                    break;
                case "_Level1-2":
                    LoadLevel2_1();
                    break;
                case "_Level2-1":
                    LoadLevel2_2();
                    break;
                case "_Level2-2":
                    LoadLevel3_1();
                    break;
                case "_Level3-1":
                    LoadLevel4_1();
                    break;
                // case "_Level1-2":
                //     LoadLevel1_2();
                //     break;
                case "_Level4-1":
                    LoadLevel4_2();
                    break;
                case "_Level4-2":
                    LoadLevels();
                    break;
            }
    }
    
    public void globalVariablesReset()
    {
        GlobalVariables.win = false;
        GlobalVariables.collisions = 0;
        GlobalVariables.platformMap = new Dictionary<string, int>();
        GlobalVariables.opTimesMap = new Dictionary<string, int>();
        GlobalVariables.failReason = "";
        GlobalVariables.portUses = new Dictionary<string, int>();
        GlobalVariables.stageTimes = new Dictionary<string, int>();
        GlobalVariables.monsterKilled = 0;
        GlobalVariables.inverseTimes = 0;
        GlobalVariables.gravityLevel = false;
        GlobalVariables.numRestart = 0;
    }

}