using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System;
using UnityEngine.SceneManagement;


[System.Serializable]
public class GameReport
{
    public string endTime;
    public string startTime;
    public string level;
    public bool success;
    public string userID;
    public string failReason;
    public int collisions;
    public int monsterKilled;
    [SerializeField]
    public List<int> numbers = new List<int>();
    [SerializeField]
    public List<string> names = new List<string>();
    [SerializeField]
    public List<int> op_times = new List<int>();
    [SerializeField]
    public List<string> portal_names = new List<string>();
    public List<int> portal_uses = new List<int>();
    public List<string> stage_names = new List<string>();
    public List<int> stage_times = new List<int>();
    public int numRestart;
    
    public string mode;

    public GameReport()
    {
        endTime = DateTime.Now.ToString();
        startTime = GlobalVariables.startTime;
        level = GlobalVariables.curLevel;
        success = GlobalVariables.win;
        userID = GlobalVariables.userID;
        collisions = GlobalVariables.collisions;
        mode = GlobalVariables.mode;
        failReason = GlobalVariables.failReason;
        monsterKilled = GlobalVariables.monsterKilled;
        numRestart = GlobalVariables.numRestart;
        foreach (var keyValuePair in GlobalVariables.platformMap)
        {
            names.Add(keyValuePair.Key);
            numbers.Add(keyValuePair.Value);
        }
        foreach (var keyValuePair in GlobalVariables.opTimesMap)
        {
            op_times.Add(keyValuePair.Value);
        }
        foreach (var keyValuePair in GlobalVariables.portUses)
        {
            portal_names.Add(keyValuePair.Key);
            portal_uses.Add(keyValuePair.Value);
        }
        foreach (var keyValuePair in GlobalVariables.stageTimes)
        {
            stage_names.Add(keyValuePair.Key);
            stage_times.Add(keyValuePair.Value);
        }
    }
}

public class DataUpload : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PostToDB()
    {
        GameReport gameReport = new GameReport();
        Debug.Log("restClient");
        RestClient.Post(
            "https://mamachick-ff15d-default-rtdb.firebaseio.com/.json",
            gameReport
        );
    }
}
