using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{

    private bool isTriggered = false; // Initial state


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Number"))
            // // if (other.gameObject.CompareTag("Number") && Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("pick");
                // Destroy(other.gameObject);
            }
            // Debug.Log("hit ground");
        }
    }


    // private void OnTriggerEnter(Collider other)
    // {
    //     if (gameObject.CompareTag("Player"))
    //     {
    //         if (other.gameObject.CompareTag("Number"))
    //         // if (other.gameObject.CompareTag("Number") && Input.GetKeyDown(KeyCode.P))
    //         {
    //             Debug.Log("pick");
    //             // Destroy(other.gameObject);
    //         }
    //         // Debug.Log("hit ground");
    //     }
    // }
}
