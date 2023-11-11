using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class BulletController : MonoBehaviour
{
    public float speed = 8f;  // Adjust this to control the bullet speed.
    public GameObject player; // Reference to the PlayerMovement script.
    private string prefabName;

    private int MaxNumber = int.MaxValue;
    private int MinNumber = int.MinValue;

    private void Start()
    {
        // Get the name of the prefab instance by accessing the name property of the GameObject.
        prefabName = gameObject.name;
    }


    public void Initialize(Vector2 direction)
    {
        // Set the initial velocity to move the bullet.
        GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        // // Check if the bullet is off-screen and destroy it.
        // void DestroyOutOfBounds()
        // {
        //     if (transform.position.x < -20 || transform.position.x > 20) // destroy it when out of the scene
        //     {
        //         Destroy(gameObject);
        //     }
        // }

        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D obstacle)
    {
        if (obstacle.gameObject.CompareTag("Gun"))
        { return; }




        if (obstacle.gameObject.CompareTag("Number") || obstacle.gameObject.CompareTag("Monster2"))
        {
            GameObject numberText = null;

            if (obstacle.gameObject.CompareTag("Number"))
            {
                Debug.Log("Bullet collided with Number");
                numberText = obstacle.gameObject.transform.Find("Number_Text").gameObject;
            }
            if (obstacle.gameObject.CompareTag("Monster2"))
            {
                numberText = obstacle.gameObject.transform.Find("Monster_Text").gameObject;
            }

            TMP_Text textComponent = numberText.GetComponent<TMP_Text>();

            if (textComponent != null)
            {
                player = GameObject.Find("Player");

                GameObject playerTextGameObject = player.transform.Find("Player_Number").gameObject;
                TMP_Text playerText = playerTextGameObject.GetComponent<TMP_Text>();

                int number = int.Parse(textComponent.text);
                int playerNumber = int.Parse(playerText.text);
                int result = 0;

                if (prefabName == "BulletAdd(Clone)")
                {
                    result = playerNumber + number;
                }
                else if (prefabName == "BulletSub(Clone)")
                {
                    result = playerNumber - number;
                }
                else if (prefabName == "BulletMultiply(Clone)")
                {
                    result = playerNumber * number;
                }
                else if (prefabName == "BulletDivide(Clone)")
                {
                    if (number != 0)
                    {
                        result = playerNumber / number;
                    }
                    else
                    {
                        result = MaxNumber;
                    }
                }

                if (result > MaxNumber)
                {
                    textComponent.text = MaxNumber.ToString();
                }
                else if (result < MinNumber)
                {
                    textComponent.text = MinNumber.ToString();
                }
                else
                {
                    textComponent.text = result.ToString();
                }
            }
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform_Solid") || other.gameObject.CompareTag("Platform_Mutate"))
        {
            Destroy(gameObject);
        }
    }

}