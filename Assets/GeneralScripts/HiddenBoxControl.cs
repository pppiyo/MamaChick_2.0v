using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class HiddenBoxControl : MonoBehaviour
{
    public List<GameObject> prefabList; // Ensure the prefabs are added in the Unity Editor

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 hitPoint = collision.contacts[0].point;
            Vector3 center = collision.collider.bounds.center;

            // Adjust the logic here according to your game's needs
            if (hitPoint.y > center.y || hitPoint.y < center.y) // Checking the collision position
            {
                Destroy(gameObject); // Destroy the current box

                // Check if there are prefabs in the list
                if (prefabList.Count > 0)
                {
                    // Select and instantiate a random prefab at the current box's position
                    GameObject selectedPrefab = prefabList[Random.Range(0, prefabList.Count)];
                    GameObject newPrefabInstance = Instantiate(selectedPrefab, transform.position, Quaternion.identity);

                    // Check if the selected prefab is named 'number'
                    if (newPrefabInstance.name.Contains("Number"))
                    {
                        // Find the TMP component and assign a random number between 1-10
                        TMP_Text numberText = newPrefabInstance.transform.Find("Number_Text")?.GetComponent<TMP_Text>();
                        if (numberText != null)
                        {
                            numberText.text = Random.Range(1, 11).ToString();
                        }
                    }
                }
            }
        }
    }
}
