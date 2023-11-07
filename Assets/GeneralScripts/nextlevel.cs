using UnityEngine;
using UnityEngine.SceneManagement;

public class nextlevel: MonoBehaviour
{
    public void LoadStartMenuScene()
    {
        SceneManager.LoadScene("_Level2");
    }
}