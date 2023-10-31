// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MoveForward : MonoBehaviour
// {
//     public float bulletSpeed = 10.0f;


//     void start()
//     {

//     }
//     // Update is called once per frame
//     void Update()
//     {
//         // if (horizontalInput != 0)
//         // {
//         //     moveDirection = new Vector3(horizontalInput, 0f, 0f);
//         //     moveDirection = moveDirection.normalized;
//         // }

//         // Vector3 spawnPosition = transform.position + (transform.localScale.x / 2.0f + 0.1f);

//         // GameObject fireInstance = Instantiate(bulletPrefab, spawnPosition + moveDirection, Quaternion.identity);

//         // Transform fireTransform = bulletPrefab.transform;

//         // fireTransform.forward = moveDirection;

//         // Rigidbody2D fireRigidbody2D = bulletPrefab.GetComponent<Rigidbody2D>();

//         // fireRigidbody2D.velocity = moveDirection * fireSpeed;
//         // // Debug.Log("Fire Direction: " + moveDirection);

//         // fireInstance.transform.eulerAngles = Vector3.zero;

//         // Calculate a position outside of the player's collision box
//         // Vector3 spawnPosition = transform.position + transform.right * (transform.localScale.x / 2.0f + 0.1f); // Offset slightly above the player
//         // Quaternion spawnRotation = transform.rotation;

//         // // Instantiate the bullet at the calculated position and rotation
//         // GameObject bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);

//         // // Get the bullet's Rigidbody2D and apply a forward force
//         // Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
//         // rb.velocity = transform.right * bulletSpeed;
//         // // to change: get where the player is facing and move in that direction
//         // transform.Translate(-transform.right * speed * Time.deltaTime);

//     }
// }
