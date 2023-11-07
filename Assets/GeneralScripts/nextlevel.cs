using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel: MonoBehaviour
{
    public void LoadNextLevel()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Check if the current scene index is valid
        if (currentSceneIndex >= 0 && currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            // Load the next scene by incrementing the current scene index
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            // Handle the case when there are no more levels
            Debug.Log("No more levels available.");
        }
    }
}
