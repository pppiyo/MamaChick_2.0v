using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("Main Menu");
    }
 public void LoadStartMenuScene()
    {
        SceneManager.LoadScene("Start Menu");
    }
public void LoadPlayScene()
    {
        SceneManager.LoadScene("Level1");
    }



}
