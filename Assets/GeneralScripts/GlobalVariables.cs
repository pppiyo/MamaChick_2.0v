using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GlobalVariables
{
    
    public static string startTime = DateTime.Now.ToString();
    
    public static string userID = Guid.NewGuid().ToString();
    
    public static string curLevel = "level 1";
    
    public static bool win = false;
    
    public static int collisions = 0;
    
    public static Dictionary<string, int> platformMap = new Dictionary<string, int>();
    
    public static Dictionary<string, int> opTimesMap = new Dictionary<string, int>();
    
    public static string failReason;
    
    public static Dictionary<string, int> portUses = new Dictionary<string, int>();

    public static string mode = "test";

    public static bool gravityLevel = false;
    
    public static int monsterKilled = 0;

    public static int inverseTimes;

}
