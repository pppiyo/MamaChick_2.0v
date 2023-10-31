using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 50f;  // Adjust this to control the bullet speed.
    public PlayerControl player; // Reference to the PlayerMovement script.


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

    void OnCollisionEnter2D(Collision2D obstacle)
    {
        // Debug.Log("Collision detected at position: " + transform.position);

        if (obstacle.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (obstacle.gameObject.CompareTag("Number"))
        {
            Destroy(gameObject);
            player.UpdateScore(obstacle);
            Destroy(obstacle.gameObject);
        }

    }




}
