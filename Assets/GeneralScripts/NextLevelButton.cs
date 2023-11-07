using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public void LoadNextLevel()
    {
        // Retrieve the previous level information
        string previousLevelName = PlayerPrefs.GetString("PreviousLevel", "DefaultStartLevel");

        // Extract the main level number and sub-level number from the previous level's name.
        if (int.TryParse(previousLevelName.Split('-')[0].Replace("_Level", ""), out int previousLevel))
        {
            int previousSubLevel = int.Parse(previousLevelName.Split('-')[1]);

            // Determine the next sub-level or main level based on the previous level and sub-level.
            int nextLevel, nextSubLevel;

            if (previousSubLevel < 2)
            {
                // If it's the first sub-level, move to the next sub-level within the same main level.
                nextLevel = previousLevel;
                nextSubLevel = previousSubLevel + 1;
            }
            else
            {
                // If it's the second sub-level, move to the next main level.
                nextLevel = previousLevel + 1;
                nextSubLevel = 1;
            }

            // Construct the scene name for the next level.
            string nextLevelSceneName = $"_Level{nextLevel}-{nextSubLevel}";

            // Check if the next level scene exists before loading it.
            if (SceneExists(nextLevelSceneName))
            {
                SceneManager.LoadScene(nextLevelSceneName);
            }
            else
            {
                // Handle the case where there is no next level (e.g., game completed).
                // You can display a message or return to the main menu.
            }
        }
    }

    // Check if a scene exists by name
    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)) == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
