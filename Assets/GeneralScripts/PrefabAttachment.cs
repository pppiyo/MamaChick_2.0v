using UnityEngine;

public class PrefabAttachment : MonoBehaviour
{
    public GameObject player;
    public GameObject prefabToAttach;

    public void AttachPrefabToPlayer()
    {
        if (player != null && prefabToAttach != null)
        {
            // Instantiate the prefab.
            GameObject newPrefabInstance = Instantiate(prefabToAttach, player.transform);

            // Set the prefab's position and rotation if needed.
            newPrefabInstance.transform.position = player.transform.position;
            newPrefabInstance.transform.rotation = player.transform.rotation;

            // Optionally, you can also modify other properties of the instantiated prefab.
            // For example:
            // newPrefabInstance.GetComponent<YourPrefabScript>().SomeFunction();

            // Make sure the instantiated prefab is properly configured and positioned as needed.
        }
        else
        {
            Debug.LogError("Player or prefab reference is null. Make sure to assign both the player and the prefab to the script.");
        }
    }

    public void RemovePrefabFromPlayer(GameObject prefabInstance)
    {
        // Example of removing the prefab from the player (but not destroying it):
        if (prefabInstance != null)
        {
            Destroy(prefabInstance);
        }
    }

    // You can add more methods or functionality as needed.
}
