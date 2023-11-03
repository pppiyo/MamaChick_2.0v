using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    private float cooldown = 0.5f;

    public PlayerControl player; // Reference to the PlayerMovement script.


    void Start()
    {
    }

    void Update()
    {
        //Press 'F' to shoot
        if (Input.GetKeyDown(KeyCode.F) && cooldown <= 0)
        {
            SpawnBullet(GetPlayerFacingDirection());
            cooldown = 0.5f;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    // If the gun collides with player, destroy the gun
    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Collision detected at position: " + transform.position);

        if (other.gameObject.CompareTag("Player"))
        {
            // Destroy(obstacle.gameObject);
            player.ShowHint("You got a gun! Press 'F' to shoot");
            StartCoroutine(player.HideHint(2));
            AttachGunToPlayer(other.gameObject);
        }
    }

    Vector2 GetPlayerFacingDirection()
    {
        // Calculate the direction the player is facing.
        Vector3 playerDirectionV3 = player.GetMoveDirection();
        Vector2 playerDirection = new Vector2(playerDirectionV3.x, playerDirectionV3.y);
        if (playerDirection == Vector2.zero)
        {
            playerDirection = Vector2.right;
        }

        return playerDirection;
    }

    void SpawnBullet(Vector2 facingDirection)
    {
        Vector3 spawnPos = new Vector3(transform.position.x + transform.localScale.x / 2 + 0.2f, transform.position.y + transform.localScale.y / 4, 0);
        // Instantiate the bullet prefab.
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

        // Call the Initialize method in the BulletController script.
        bullet.GetComponent<BulletController>().Initialize(facingDirection);
    }


    private void AttachGunToPlayer(GameObject player)
    {
        if (player != null && gameObject != null)
        {

            // Set the local position of the gun relative to the player (adjust these values as needed).
            gameObject.transform.localPosition = new Vector3(player.transform.position.x +
            player.transform.localScale.x / 2 + gameObject.transform.localScale.x / 2 + 0.7f,
            player.transform.position.y, 0);

            // Set the gun's parent to the player's GameObject, making it move with the player.
            gameObject.transform.SetParent(player.transform);
        }
        else
        {
            Debug.LogError("Player or prefab reference is null. Make sure to assign both the player and the prefab to the script.");
        }
    }



}
