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
    public int collisions;
    [SerializeField]
    public List<int> numbers = new List<int>();
    [SerializeField]
    public List<string> names = new List<string>();
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
        foreach (var keyValuePair in GlobalVariables.platformMap)
        {
            names.Add(keyValuePair.Key);
            numbers.Add(keyValuePair.Value);
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
        if (GlobalVariables.win)
        {
            PostToDB();
            GlobalVariables.win = false;
            GlobalVariables.collisions = 0;
        }
    }

    private void PostToDB()
    {
        GameReport gameReport = new GameReport();
        Debug.Log("restClient");
        RestClient.Post(
            "https://mamachick-ff15d-default-rtdb.firebaseio.com/.json",
            gameReport
        );
    }
}
