using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using TMPro;

public class BulletController : MonoBehaviour
{
    public float speed = 8f;  // Adjust this to control the bullet speed.
    public GameObject player; // Reference to the PlayerMovement script.

    public void Initialize(Vector2 direction)
    {
        // Set the initial velocity to move the bullet.
        GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the bullet is off-screen and destroy it.
        void DestroyOutOfBounds()
        {
            if (transform.position.x < -20 || transform.position.x > 20) // destroy it when out of the scene
            {
                Destroy(gameObject);
            }
        }

        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D obstacle)
    {
        if (obstacle.gameObject.CompareTag("Number"))
        {
            GameObject numberText = obstacle.gameObject.transform.Find("Number_Text").gameObject;

            TMP_Text textComponent = numberText.GetComponent<TMP_Text>();
            // Debug.Log("Number collided: " + textComponent.text);
            if (textComponent != null)
            {
                // Debug.Log("player: " + player);

                GameObject playerTextGB = player.transform.Find("Player_Number").gameObject;
                TMP_Text playerText = playerTextGB.GetComponent<TMP_Text>();
                textComponent.text = playerText.text; // Change the text to whatever you want.
            }
        }

        Destroy(gameObject);
    }




}
