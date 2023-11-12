using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GunController : MonoBehaviour
{
    public PlayerControl player; // Reference to the PlayerMovement script.
    public GameObject bulletAddPrefab;
    public GameObject bulletSubPrefab;
    public GameObject bulletMultiplyPrefab;
    public GameObject bulletDividePrefab;
    private GameObject bulletPrefab;
    private GameObject bullet;
    // public DragAndShoot dragAndShoot;
    private float cooldown = 0.5f;
    private bool isEquipped = false;
    // private GameObject numberPrefab = null;
    private int shootCnt = 0;
    public int maxBulletCnt = 10;

    void Start()
    {
        // numberBalls = GameObject.Find("Number");
        // numberPrefab = LoadPrefab("Assets/Prefabs/Number.prefab");

    }

    void Update()
    {
        //Press 'F' to shoot
        if (isEquipped && Input.GetKeyDown(KeyCode.F) && cooldown <= 0)
        {
            // numberPrefab.GetComponent<Rigidbody2D>().isKinematic = true;
            // numberPrefab.GetComponent<Collider2D>().isTrigger = true;

            if (shootCnt == maxBulletCnt)
            {
                player.ShowHint("No Bullet Left");
                StartCoroutine(player.HideHint(3));
                return;
            }
            else
            {
                shootCnt += 1;
                SpawnBullet(GetPlayerFacingDirection());
                cooldown = 0.5f;

                player.ShowHint("You have " + (maxBulletCnt - shootCnt) + " bullets left");
                StartCoroutine(player.HideHint(3));
                player.HideHint(1);
            }
        }
        else
        {
            cooldown -= Time.deltaTime;
            // numberPrefab.GetComponent<Rigidbody2D>().isKinematic = false;
            // numberPrefab.GetComponent<Collider2D>().isTrigger = false;
        }
    }



    // If the gun collides with player, destroy the gun
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.ShowHint("You got a gun! Press 'F' to shoot");
            StartCoroutine(player.HideHint(3));
            AttachGunToPlayer(other.gameObject);
            isEquipped = true;
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


    // spawn bullet at the player/gun's facing position
    void SpawnBullet(Vector2 facingDirection)
    {
        Vector3 spawnPos = new Vector3(transform.position.x + transform.localScale.x / 2 + 0.2f, transform.position.y + transform.localScale.y / 4, 0);

        // Instantiate the bullet prefab.
        bulletPrefab = GetBulletType(player);


        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab is null. Make sure to assign the prefab to the script.");
        }
        else
        {
            bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            // Call the Initialize method in the BulletController script.
            bullet.GetComponent<BulletController>().Initialize(facingDirection);
        }

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


    private GameObject GetBulletType(PlayerControl player)
    {
        // Debug.Log("Confirming player id: " + player.operatorID);
        // operatorID; // 0: add; 1: sub; 2: multiply; 3: divide
        if (player.operatorID == 0)
        {
            return bulletAddPrefab;
        }
        else if (player.operatorID == 1)
        {
            return bulletSubPrefab;
        }
        else if (player.operatorID == 2)
        {
            return bulletMultiplyPrefab;
        }
        else if (player.operatorID == 3)
        {
            return bulletDividePrefab;
        }
        else
        {
            return null;
        }
    }

    // #if UNITY_EDITOR
    //     GameObject LoadPrefab(string path)
    //     {
    //         return AssetDatabase.LoadAssetAtPath<GameObject>(path);
    //     }
    // #endif
}