using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GlobalVariables
{
    public static string startTime = DateTime.Now.ToString();
    
    public static string userID = Guid.NewGuid().ToString();
    
    public static string curLevel = "level 1";
    
}
