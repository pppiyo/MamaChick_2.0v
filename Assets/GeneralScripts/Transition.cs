using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public void LoadLevels()
    {
        SceneManager.LoadScene("LevelSelection");
    }
    
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}