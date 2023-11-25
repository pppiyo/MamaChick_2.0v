using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class BulletController : MonoBehaviour
{
    public float speed = 8f;  // Adjust this to control the bullet speed.
    private GameObject player; // Reference to the PlayerMovement script.
    private string prefabName;
    private GameObject nearestNumber = null;
    private int MaxNumber = int.MaxValue;
    private int MinNumber = int.MinValue;
    // code for avoiding conflict with Drag and shoot


    private void Start()
    {
        // Get the name of the prefab instance by accessing the name property of the GameObject.
        prefabName = gameObject.name;
        nearestNumber = null;
        player = GameObject.Find("Player");
    }


    public void Initialize(Vector2 direction)
    {
        // Set the initial velocity to move the bullet.
        GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
    }


    // Update is called once per frame
    void Update()
    {
        nearestNumber = null;
        // manually set collision event
        FindnearestNumber();

        if (nearestNumber)
        {
            UpdatenearestNumberText();
            Destroy(gameObject);
        }

        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }

    }


    private void OnCollisionEnter2D(Collision2D obstacle)
    {
        if (obstacle.gameObject.CompareTag("Gun"))
        { return; }

        if (!obstacle.gameObject.CompareTag("Monster2"))
        {
            Destroy(gameObject);
        }
    }


    private void FindnearestNumber()
    {
        GameObject[] numbers = GameObject.FindGameObjectsWithTag("Number");

        if (numbers.Length == 0)
        {
            // 如果没有球体对象，不执行任何操作
            return;
        }

        // 计算最近的球体对象
        float minDistance = 1.0f;
        nearestNumber = null;

        foreach (GameObject number in numbers)
        {
            float distance = Vector3.Distance(transform.position, number.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNumber = number;
            }
        }
    }

    // Bullet interaction with Number (counterpart for monster2 is in Monster2.cs)
    private void UpdatenearestNumberText()
    {
        GameObject numberText = nearestNumber.gameObject.transform.Find("Number_Text").gameObject;

        TMP_Text textComponent = numberText.GetComponent<TMP_Text>();
        Debug.Log("textComponent: " + textComponent.text);


        if (textComponent != null)
        {
            GameObject playerTextGameObject = player.transform.Find("Player_Number").gameObject;
            TMP_Text playerText = playerTextGameObject.GetComponent<TMP_Text>();

            int number = int.Parse(textComponent.text);
            int playerNumber = int.Parse(playerText.text);
            // Debug.Log("playerNumber: " + playerNumber);

            int result = 0;

            if (prefabName == "BulletAdd(Clone)")
            {
                // result = playerNumber + number;
                // Handle overflow
                if (number + playerNumber > MaxNumber)
                {
                    // handle overflow: let result = MaxNumber
                    result = MaxNumber;
                }
                else
                {
                    result = playerNumber + number;
                }
            }
            else if (prefabName == "BulletSub(Clone)")
            {
                // result = playerNumber - number;
                // Handle overflow
                if (playerNumber - number < MinNumber)
                {
                    // handle overflow: let currentX = MinNumber
                    result = MinNumber;
                }
                else
                {
                    result = playerNumber - number;
                }
            }
            else if (prefabName == "BulletMultiply(Clone)")
            {
                // result = playerNumber * number;
                // Handle overflow
                if (playerNumber * number > MaxNumber)
                {
                    // handle overflow: let currentX = MaxNumber
                    result = MaxNumber;
                }
                else if (playerNumber * number < MinNumber)
                {
                    // handle overflow: let currentX = MinNumber
                    result = MinNumber;
                }
                else
                {
                    result = playerNumber * number;
                }
            }
            else if (prefabName == "BulletDivide(Clone)")
            {
                if (number != 0)
                {
                    result = playerNumber / number;
                }
                else
                {
                    if (playerNumber > 0)
                    {
                        result = MaxNumber;
                    }
                    else if (playerNumber < 0)
                    {
                        result = MinNumber;
                    }
                    else // playerNumber == 0
                    {
                        result = 0;
                    }
                }
            }

            // Debug.Log("!!!!!result is: " + result);

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

}