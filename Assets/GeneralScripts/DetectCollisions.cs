using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private LayerMask playerLayer;
    private LayerMask numberLayer;

    public static DetectCollisions Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void IgnorePlayerNumberCollision()
    {
        // Handle the collision event, such as collecting the token.
        // You can access the player's script or GameObject to update its state.
        // For example, increment a score, play an effect, and destroy the token.

        playerLayer = LayerMask.NameToLayer("Player");
        numberLayer = LayerMask.NameToLayer("Number");
        Physics2D.IgnoreLayerCollision(playerLayer, numberLayer, true); // Ignore collisions between layer1 and layer2
        Debug.Log("OnPlayerNumberCollision Detected");
    }


    private void OnCollisionEnter2D(Collision2D other)
    {

        if (gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Number"))
            {
                IgnorePlayerNumberCollision();
            }
        }
    }



}
