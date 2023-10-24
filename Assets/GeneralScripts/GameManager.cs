using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 声明一个静态实例，以便其他类可以访问
    public static GameManager instance;
    public string tagToCount = "Number";

    public Text countdownText;

    private bool isGameOver = false;

    void Start()
    {
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (CountLeftNumbers() == 0) // + or Can't arrive at the destination
            {
                LoseGame();
            }
            else
            {
                WinGame();
            }
        }
    }

    private void LoseGame()
    {
        isGameOver = true;
        SceneManager.LoadScene("GameOverScene");
    }

    public void WinGame()
    {
        isGameOver = true;
        SceneManager.LoadScene("WinScene");
    }

    // If there're no numbers left on the screen, the game ends.
    private int CountLeftNumbers()
    {
        return CountObjectsWithTag(tagToCount);
    }

    private int CountObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        return objects.Length;
    }
}
