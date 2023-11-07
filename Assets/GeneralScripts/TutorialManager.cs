using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    private string level;
    private int stage;
    private GameObject Player;
    private GameObject MenuWheel;
    private GameObject[] tutorialElements;

    //Variables for Level1 Progress Tracking
    private string currentStage;
    private int level1Stage0;
    private int level1Stage1;
    private int level1Stage2;
    private int level1Stage3;
    private int level1Stage4;
    private int level1Stage5;
    private string currentProgression;

    // Individual Level Components
    private GameObject Akey;
    private GameObject Dkey;
    private GameObject MoveInstruction;
    private GameObject Spacekey;
    private GameObject JumpInstruction;
    private GameObject Leftkey;
    private GameObject Rightkey;
    private GameObject UPkey;
    private GameObject DOWNkey;
    private GameObject OperationInstruction;
    private GameObject Platform_1_1;
    private GameObject PlatformInstruction;
    private GameObject Checkpoint1;
    private GameObject Platform_1_2;
    private GameObject Platform_1_3;
    private GameObject Stage3Objects;
    private GameObject SpikeInstruction;
    private GameObject Stage4Objects;
    private GameObject AddInstruction;
    private GameObject EpressInstruction;
    private GameObject fakePlatformInstruction;
    private GameObject MenuInstruction;
    private GameObject Stage5Objects;
    private GameObject Platform_1_7;
    private GameObject Platform_1_8;
    private GameObject Platform_1_9;
    private GameObject Platform_1_10;
    private GameObject Destination;

    // Variables for level 2 tracking
    private int level2Stage0;
    private int level2Stage1;
    public GameObject Number2_1;
    public GameObject Number2_2;
    public GameObject Number3_1;

    //Miscelleneous Variables for different functions
    private bool shouldWait;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise the level value based on the loaded tutorial level.
        // Stage value is 0 for any level's beginning
        level = GlobalVariables.curLevel;
        stage = 1;
        Player = GameObject.Find("Player");
        MenuWheel = GameObject.Find("MenuWheel");
        Stage3Objects = GameObject.Find("Stage3");
        Stage4Objects = GameObject.Find("Stage4");
        Stage5Objects = GameObject.Find("Stage5");
        
        level1Stage0 = 0;
        level1Stage1 = 0;
        level1Stage2 = 0;
        level1Stage3 = 0;
        level1Stage4 = 0;
        level1Stage5 = 0;
        currentProgression = "";

        // Level 2 Initialisation
        level2Stage0 = 0;
        level2Stage1 = 0;
        Platform_1_9 = GameObject.Find("Platform_1_9");

        // Initialise Level Components
        Akey = GameObject.Find("Akey");
        Dkey = GameObject.Find("Dkey");
        MoveInstruction = GameObject.Find("MoveInstruction");
        Spacekey = GameObject.Find("Spacekey");
        JumpInstruction = GameObject.Find("JumpInstruction");
        Leftkey = GameObject.Find("Leftkey");
        Rightkey = GameObject.Find("Rightkey");
        UPkey = GameObject.Find("UPkey");
        DOWNkey = GameObject.Find("DOWNkey");
        OperationInstruction = GameObject.Find("OperationInstruction");
        Platform_1_1 = GameObject.Find("Platform_1_1");
        PlatformInstruction = GameObject.Find("PlatformInstruction");
        Platform_1_2 = GameObject.Find("Platform_1_2");
        Platform_1_3 = GameObject.Find("Platform_1_3");
        Destination = GameObject.Find("Destination");
        Checkpoint1 = GameObject.Find("Checkpoint1");
        SpikeInstruction = GameObject.Find("SpikeInstruction");
        fakePlatformInstruction = GameObject.Find("fakePlatformInstruction");
        MenuInstruction = GameObject.Find("MenuInstruction");
        AddInstruction = GameObject.Find("AddInstruction");
        EpressInstruction = GameObject.Find("EpressInstruction");
        Platform_1_7 = GameObject.Find("Platform_1_7");
        Platform_1_8 = GameObject.Find("Platform_1_8");
        Platform_1_9 = GameObject.Find("Platform_1_9");
        Platform_1_10 = GameObject.Find("Platform_1_10");

        // Level 4 is simple in text for now
        if (GlobalVariables.curLevel == "tutorial 4")
        {
            return;
        }
        // Hide all the tutorial elements initially
        tutorialElements = GameObject.FindGameObjectsWithTag("Tutorial_key");
        foreach (GameObject obj in tutorialElements)
        {
           obj.SetActive(false);
        }
        tutorialElements = GameObject.FindGameObjectsWithTag("Tutorial_Text");
        foreach (GameObject obj in tutorialElements)
        {
            obj.SetActive(false);
        }
        tutorialElements = GameObject.FindGameObjectsWithTag("Tutorial_object");
        foreach (GameObject obj in tutorialElements)
        {
            obj.SetActive(false);
        }
        tutorialElements = GameObject.FindGameObjectsWithTag("Platform_Solid");
        foreach (GameObject obj in tutorialElements)
        {
            obj.SetActive(false);
        }
        tutorialElements = GameObject.FindGameObjectsWithTag("Platform_Mutate");
        foreach (GameObject obj in tutorialElements)
        {
            obj.SetActive(false);
        }
        tutorialElements = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject obj in tutorialElements)
        {
            obj.SetActive(false);
        }
        tutorialElements = GameObject.FindGameObjectsWithTag("Destination");
        foreach (GameObject obj in tutorialElements)
        {
            obj.SetActive(true);
        }

        // Miscelleneous Objects
        //MenuWheel.SetActive(false);
        Stage4Objects.SetActive(true);
        Stage5Objects.SetActive(true);
    }

    // Delays transitions between stages
    IEnumerator StageDelayer()
    {
        if (shouldWait)
        {
            shouldWait = false;
            yield return new WaitForSeconds(0.5f);
            switch (currentStage)
            {
                case "Level1Stage0":
                    level1Stage0++;
                    break;
                case "Level1Stage1":
                    level1Stage1++;
                    break;
                case "Level1Stage2":
                    level1Stage2++;
                    break;
            }
        }
    }

    //activateLevel#Stage# function activates necessary elements of the corresponding level and stage
    //deactivateLevel#Stage# function deactivates necessary elements of the corresponding level and stage

    void activateLevel1Stage0()
    {
        currentStage = "Level1Stage0";
        Akey.SetActive(true);
        Dkey.SetActive(true);
        MoveInstruction.SetActive(true);
        level1Stage0 = 1;
        currentProgression = "";
    }

    void deactivateLevel1Stage0()
    {
        Akey.SetActive(false);
        Dkey.SetActive(false);
        MoveInstruction.SetActive(false);
    }

    void activateLevel1Stage1()
    {
        currentStage = "Level1Stage1";
        Spacekey.SetActive(true);
        JumpInstruction.SetActive(true);
        level1Stage1 = 1;
    }

    void deactivateLevel1Stage1()
    {
        Spacekey.SetActive(false);
        JumpInstruction.SetActive(false);
    }

    void activateLevel1Stage2()
    {
        currentStage = "Level1Stage2";
        Platform_1_1.SetActive(true);
        Platform_1_3.SetActive(true);
        Checkpoint1.SetActive(true);
        PlatformInstruction.SetActive(true);
        level1Stage2 = 1;
    }

    void deactivateLevel1Stage2()
    {
        PlatformInstruction.SetActive(false);
        level1Stage2 = 3;
    }

    void activateLevel1Stage3()
    {
        currentStage = "Level1Stage3";
        Stage3Objects.SetActive(true);
        SpikeInstruction.SetActive(true);
        level1Stage3 = 1;
    }

    void deactivateLevel1Stage3()
    {
        SpikeInstruction.SetActive(false);
        level1Stage3 = 3;
    }

    void activateLevel1Stage4()
    {
        currentStage = "Level1Stage4";
        Stage4Objects.SetActive(true);
        level1Stage4 = 1;
    }

    void deactivateLevel1Stage4()
    {
        SpikeInstruction.SetActive(false);
        fakePlatformInstruction = GameObject.Find("fakePlatformInstruction");
        AddInstruction.SetActive(false);
        EpressInstruction.SetActive(false);
        fakePlatformInstruction.SetActive(false);
        level1Stage4 = 3;
    }

    void activateLevel1Stage5()
    {
        currentStage = "Level1Stage5";
        Stage5Objects.SetActive(true);
        UPkey.SetActive(true);
        DOWNkey.SetActive(true);
        Leftkey.SetActive(true);
        Rightkey.SetActive(true);
        MenuInstruction.SetActive(true);
        Platform_1_7.SetActive(true);
        Platform_1_8.SetActive(true);
        Platform_1_9.SetActive(true);
        Platform_1_10.SetActive(true);
        Destination.SetActive(true);
        level1Stage4 = 5;
    }

    void deactivateLevel1Stage5()
    {
        UPkey.SetActive(false);
        DOWNkey.SetActive(false);
        Akey.SetActive(false);
        Dkey.SetActive(false);
        MenuInstruction.SetActive(false);
        level1Stage5 = 3;
    }

    void level1Manager()
    {
        // Stage 0
        switch (level1Stage0)
        {
            case 0:
                activateLevel1Stage0();
                break;
            case 1:
                if (Input.GetKey(KeyCode.A) && !Regex.IsMatch(currentProgression, ".*A.*"))
                {
                    currentProgression += "A";
                }
                if (Input.GetKey(KeyCode.D) && !Regex.IsMatch(currentProgression, ".*D.*"))
                {
                    currentProgression += "D";
                }
                if(currentProgression == "AD" || currentProgression == "DA")
                {
                    level1Stage0 = 2;
                    shouldWait = true;
                }
                break;
            case 2:
                deactivateLevel1Stage0();
                StartCoroutine(StageDelayer());
                break;
            case 3:
                // Stage 1
                switch (level1Stage1)
                {
                    case 0:
                        activateLevel1Stage1();
                        break;
                    case 1:
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            level1Stage1 = 2;
                            shouldWait = true;
                        }
                        break;
                    case 2:
                        deactivateLevel1Stage1();
                        StartCoroutine(StageDelayer());
                        break;
                    case 3:
                        //Stage 2
                        switch (level1Stage2)
                        {
                            case 0:
                                activateLevel1Stage2();
                                break;
                            case 1:
                                // Wait for crossing checkpoint 1
                                if (Player.transform.position.x > 7)
                                {
                                    level1Stage2 = 2;
                                }
                                break;
                            case 2:
                                deactivateLevel1Stage2();
                                break;
                            case 3:
                                // Stage 3
                                switch (level1Stage3)
                                {
                                    case 0:
                                        activateLevel1Stage3();
                                        break;
                                    case 1:
                                        // Wait for crossing checkpoint 2
                                        if (Player.transform.position.x > 30)
                                        {
                                            level1Stage3 = 2;
                                        }
                                        break;
                                    case 2:
                                        deactivateLevel1Stage3();
                                        break;
                                    case 3:
                                        // Stage 4
                                        switch (level1Stage4)
                                        {
                                            case 0:
                                                activateLevel1Stage4();
                                                break;
                                            case 1:
                                                if(Player.transform.position.x > 50)
                                                {
                                                    level1Stage4 = 2;
                                                }
                                                break;
                                            case 2:
                                                deactivateLevel1Stage4();
                                                break;
                                            case 3:
                                                // Stage 5
                                                switch (level1Stage5)
                                                {
                                                    case 0:
                                                        activateLevel1Stage5();
                                                        break;
                                                    case 1:
                                                        break;
                                                    case 2:
                                                        break;
                                                    case 3:
                                                        break;
                                                }
                                                break;
                                        }
                                        break;
                                }
                                break;
                        }
                        break;
                }
                break;
        }
    }

    void activateLevel2Stage0()
    {
        Platform_1_1.SetActive(true);
        Platform_1_3.SetActive(true);
        Platform_1_10.SetActive(true);
        JumpInstruction.SetActive(true);
        Checkpoint1.SetActive(true);
        level2Stage0 = 1;
    }

    void deactivateLevel2Stage0()
    {
        level2Stage0 = 3;
    }

    void activateLevel2Stage1()
    {
        MenuInstruction.SetActive(true);
    }

    void deactivateLevel2Stage1()
    {

    }

    void level2Manager()
    {
        Platform_1_7.SetActive(true);
        Platform_1_8.SetActive(true);
        Platform_1_9.SetActive(true);
        if (Player.transform.position.x > 10)
        {
            JumpInstruction.SetActive(false);
        }
        switch (level2Stage0)
        {
            case 0:
                activateLevel2Stage0();
                break;
            case 1:
                if(Player.transform.position.x > 20)
                {
                    OperationInstruction.SetActive(true);
                }
                if(Player.transform.position.y > 5)
                {
                    OperationInstruction.SetActive(false);
                    level2Stage0 = 2;
                }
                break;
            case 2:
                if (!Number2_1.activeSelf && !Number2_2.activeSelf && !Number3_1.activeSelf)
                {/*
                    if (Player.GetComponent<PlayerControl>().currentX < 9)
                    {
                        Player.transform.position = GameObject.Find("Checkpoint1").transform.position;
                        Player.GetComponent<PlayerControl>().currentX = 0;
                        GameObject.Find("Player_Number").GetComponent<TMP_Text>().text = "0";
                        SpikeInstruction.SetActive(true);
                        Number2_1.SetActive(true);
                        Number2_2.SetActive(true);
                        Number3_1.SetActive(true);
                    }*/
                }
                if (Player.transform.position.x > 60)
                {
                    SpikeInstruction.SetActive(false);
                    deactivateLevel2Stage0();
                }
                break;
            case 3:
                switch (level2Stage1)
                {
                    case 0:
                        activateLevel2Stage1();
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
        }
    }

    void activateLevel3Stage0()
    {

    }

    void deactivateLevel3Stage0()
    {

    }

    void level3Manager()
    {

    }

    void activateLevel4Stage0()
    {

    }

    void deactivateLevel4Stage0()
    {

    }

    void level4Manager()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (level)
        {
            case "tutorial 1":
                level1Manager();
                break;
            case "tutorial 2":
                level2Manager();
                break;
            case "tutorial 3":
                level3Manager();
                break;
            case "tutorial 4":
                level4Manager();
                break;
            default:
                level2Manager();
                break;
        }
    }
}
