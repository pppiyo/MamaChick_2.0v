using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    public GameObject helpPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        helpPanel.SetActive(false);
    }

    public void ToggleHelpPanel()
    {
        helpPanel.SetActive(!helpPanel.activeSelf);
    }

    public void cancelHelpPanel()
    {
        helpPanel.SetActive(false);
    }
}
