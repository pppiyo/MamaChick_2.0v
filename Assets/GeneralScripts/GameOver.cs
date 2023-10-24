using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // You can call this method when a game over condition is met, e.g., when the player loses all lives.
    public void LoadHomeScene()
    {
       
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadRestartScene()
    {

        SceneManager.LoadScene("Level1");
    }



}
