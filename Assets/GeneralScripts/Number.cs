using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    // Reference to the Rigidbody component
    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody component on the prefab
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        //Press 'F' to shoot
        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     Debug.Log("F pressed");
        //     if (rb != null)
        //     {
        //         // Disable physics simulation (make the Rigidbody kinematic)
        //         rb.isKinematic = true;
        //     }
        // }

        // //Press 'F' to shoot
        // if (Input.GetKeyUp(KeyCode.F))
        // {
        //     if (rb != null)
        //     {
        //         // Enable physics simulation (make the Rigidbody kinematic)
        //         rb.isKinematic = false;
        //     }
        // }

    }

}

