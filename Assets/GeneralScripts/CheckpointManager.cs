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
                MenuWheel = GameObject.Find("MenuWheel");
                MenuWheel.SetActive(false);
                AddInstruction = GameObject.Find("AddInstruction");
                AddInstruction.SetActive(false);
                EpressInstruction = GameObject.Find("EpressInstruction");
                EpressInstruction.SetActive(false);
                Upkey = GameObject.Find("UPkey");
                Upkey.SetActive(false);
                fakePlatformInstruction.SetActive(false);
                Stage4_1 = GameObject.Find("Stage4_1");
                Stage4_1.SetActive(false);
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
                    transform.position = GameObject.Find("Checkpoint1").transform.position;
                    break;
                case "Stage4":
                    transform.position = GameObject.Find("Checkpoint2").transform.position;
                    fakePlatformInstruction.SetActive(true);
                    AddInstruction.SetActive(true);
                    EpressInstruction.SetActive(true);
                    Upkey.SetActive(true);
                    Stage4_1.SetActive(true);
                    MenuWheel.SetActive(true);
                    break;
                case "Stage5":
                    transform.position = GameObject.Find("Checkpoint3").transform.position;
                    GameObject.Find("Player").GetComponent<PlayerControl>().currentX = 0;
                    GameObject.Find("Player_Number").GetComponent<TMP_Text>().text = "0";
                    GameObject.Find("Player").GetComponent<PlayerControl>().resolvePlatforms();
                    break;
            }
        }

        if (GlobalVariables.curLevel == "tutorial 2" && obstacle.gameObject.CompareTag("Spike"))
        {
            switch (obstacle.gameObject.transform.parent.name)
            {
                case "Stage0":
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                case "Stage4":
                    transform.position = GameObject.Find("Checkpoint1").transform.position;
                    break;
                case "Stage5":
                    transform.position = GameObject.Find("Checkpoint2").transform.position;
                    break;
            }
        }

        if (GlobalVariables.curLevel == "tutorial 3" && obstacle.gameObject.CompareTag("Spike"))
        {
            switch (obstacle.gameObject.transform.parent.name)
            {
                case "Stage0":
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                case "Stage4":
                    transform.position = GameObject.Find("Checkpoint1").transform.position;
                    break;
            }
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
}
