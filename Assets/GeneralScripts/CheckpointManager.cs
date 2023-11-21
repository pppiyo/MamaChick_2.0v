using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    private string currentLevel;

    //tutorialInstruction sets for checkpoints
    private GameObject fakePlatformInstruction;
    private GameObject Stage4_1;
    private GameObject AddInstruction;
    private GameObject EpressInstruction;
    private GameObject Upkey;
    private GameObject MenuWheel;

    // Start is called before the first frame update
    void Start()
    {
        switch (GlobalVariables.curLevel)
        {
            case "tutorial 1":
                currentLevel = "tutorial";
                fakePlatformInstruction = GameObject.Find("fakePlatformInstruction");
                AddInstruction = GameObject.Find("AddInstruction");
                AddInstruction.SetActive(false);
                EpressInstruction = GameObject.Find("EpressInstruction");
                EpressInstruction.SetActive(false);
                Upkey = GameObject.Find("UPkey");
                Upkey.SetActive(false);
                fakePlatformInstruction.SetActive(false);
                break;
            default:
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D obstacle)
    {
        if (GlobalVariables.curLevel == "tutorial 1" && obstacle.gameObject.CompareTag("Spike"))
        {
            switch (obstacle.gameObject.transform.parent.name)
            {
                case "Stage3":
                    increaseStage("Stage3");
                    transform.position = GameObject.Find("Checkpoint1").transform.position;
                    break;
            }
        }

        if (GlobalVariables.curLevel == "tutorial 2" && obstacle.gameObject.CompareTag("Spike"))
        {
            switch (obstacle.gameObject.transform.parent.name)
            {
                case "Stage0":
                    increaseStage("Stage0");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                case "Stage4":
                    increaseStage("Stage4");
                    transform.position = GameObject.Find("Checkpoint1").transform.position;
                    break;
                case "Stage5":
                    increaseStage("Stage5");
                    transform.position = GameObject.Find("Checkpoint2").transform.position;
                    break;
            }
        }

        if (GlobalVariables.curLevel == "tutorial 3" && obstacle.gameObject.CompareTag("Spike"))
        {
            switch (obstacle.gameObject.transform.parent.name)
            {
                case "Stage0":
                    increaseStage("Stage0");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                case "Stage4":
                    increaseStage("Stage4");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    GameObject.Find("TutorialInstructions").GetComponent<TutorialManager>().level3Stage0 = 3;
                    GameObject.Find("TutorialInstructions").GetComponent<TutorialManager>().level3Stage1 = 3;
                    transform.position = GameObject.Find("Checkpoint1").transform.position;
                    break;
            }
        }

        if(GlobalVariables.curLevel == "tutorial 4" && obstacle.gameObject.CompareTag("Spike"))
        {
            increaseStage("Stage_Tutorial4");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (GlobalVariables.curLevel == "tutorial 5" && obstacle.gameObject.CompareTag("Spike"))
        {
            increaseStage("Stage_Tutorial5");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        switch (currentLevel)
        {
            case "tutorial":
                break;
        }
    }

    void increaseStage(string stageName)
    {
        if (GlobalVariables.stageTimes.ContainsKey(stageName))
        {
            GlobalVariables.stageTimes[stageName]++;
        }
        else
        {
            GlobalVariables.stageTimes[stageName] = 1;
        }
    }
}
