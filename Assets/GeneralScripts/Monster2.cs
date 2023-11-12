using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Monster2 : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f; // speed of Monster2 movement
    private float leftBoundary; // movement left boundary: align with left edge of platform the monster is on
    private float rightBoundary; // // movement right boundary: align with left edge of platform the monster is on
    [SerializeField]
    private int condition = 1; // ?
    private GameObject SceneLoader;
    private GameObject nearestPlatform; // Reference to the platform the monster is on
    private GameObject[] allPlatforms;
    private int direction = 1; // up: 1, down: -1 

    public float scaleMultiplier = 0.1f;  // Adjust this to control the scaling rate.
    private int MaxNumber = int.MaxValue;
    private int MinNumber = int.MinValue;
    private Vector3 initialScale;
    private float initialHeight;
    private Vector3 initialPosition;
    private float yOffset;
    private GameObject player;
    private TMP_Text numberText; // Reference to the TMP_Text component with the number.
    private int number; // The real value of the monster.
    private int MAX_SIZE_NUMBER = 40; // the max size facotr monster2 can get. (in terms of appearance cannot be bigger than this number's corresponding size)


    void Start()
    {
        SceneLoader = GameObject.Find("SceneManager");
        player = GameObject.Find("Player");

        GetNumberText(); // get the number of the monster from the text component

        GetInitialPositionPosition();
        FindNearestPlatform();
        GetBoundary();
    }


    void Update()
    {
        number = int.Parse(numberText.text);
        UpdateSize();
        UpdateMovement();
    }


    private void GetInitialPositionPosition()
    {
        // Record the initial scale, height, and position of the GameObject.
        initialScale = transform.localScale;
        initialHeight = numberText.bounds.size.y;
        initialPosition = transform.position;
    }

    // Get TMP_Text on Monster2
    private void GetNumberText()
    {
        numberText = gameObject.transform.Find("Monster_Text").GetComponent<TMP_Text>();

        // Ensure you have a reference to the Text component.
        if (numberText == null)
        {
            Debug.LogError("Number Text component is missing.");
            return;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            string prefabName = other.gameObject.name;

            // Debug.Log("prefab name is: " + prefabName);
            Debug.Log("monster 2 Text: " + number);

            // get player number text // good
            string playerText = player.transform.Find("Player_Number").gameObject.GetComponent<TMP_Text>().text;
            int playerNumber = int.Parse(playerText); // good

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
            // // Debug.Log(result);

            if (result > MaxNumber)
            {
                numberText.text = MaxNumber.ToString();
            }
            else if (result > 0 && result <= MaxNumber)
            {
                numberText.text = result.ToString();
            }
            else
            {
                numberText.text = "0";
            }

            Destroy(other.gameObject);
        }

        // Debug.Log("hi");
        if (other.gameObject.CompareTag("Elevator2"))
        {
            Debug.Log("monster2 collided with elevator2");
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }

    }



    private void UpdateSize()
    {
        int sizeNumber = number;
        if (number > MAX_SIZE_NUMBER)
        {
            sizeNumber = MAX_SIZE_NUMBER;
        }

        // Calculate the new scale based on the number for both width and height.
        float scaleFactor = sizeNumber * scaleMultiplier;

        // Apply the scale factor to the initial scale.
        Vector3 newScale = new Vector3(initialScale.x * scaleFactor, initialScale.y * scaleFactor, initialScale.z);

        // Set the new scale to the transform.
        transform.localScale = newScale;

        // Since we are scaling uniformly and the pivot is at the center, the object will grow equally in all directions.
        // To keep the bottom in the same position, we need to move the object up by half the increased height.
        float heightIncrease = (initialScale.y * scaleFactor) - initialScale.y;
        float newYPosition = initialPosition.y + heightIncrease / 2;

        // Set the new position to the transform.
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }


    private void UpdateMovement()
    {
        if (number >= MAX_SIZE_NUMBER)
        {
            // Debug.Log("actualValue: " + actualValue);
            return;
        }
        else
        {
            Vector2 currentPosition = transform.position;
            // Debug.Log("currentPosition: " + currentPosition);

            // // 根据方向和速度更新位置
            currentPosition.x += moveSpeed * direction * Time.deltaTime;
            currentPosition.y += yOffset;
            transform.position = currentPosition;

            // // 检查是否达到边界，如果是，改变方向
            if (currentPosition.x <= leftBoundary)
            {
                direction = 1; // 向右
            }
            else if (currentPosition.x >= rightBoundary)
            {
                direction = -1; // 向左
            }
        }
    }


    private void GetBoundary()
    {
        if (nearestPlatform != null)
        {
            Transform platformTransform = nearestPlatform.transform;

            Renderer platformRenderer = nearestPlatform.GetComponent<Renderer>();
            Renderer monsterRenderer = gameObject.GetComponent<Renderer>();

            // Calculate the left and right boundaries of the platform
            leftBoundary = platformTransform.position.x - platformRenderer.bounds.size.x / 2 + monsterRenderer.bounds.size.x / 2;
            rightBoundary = platformTransform.position.x + platformRenderer.bounds.size.x / 2 - monsterRenderer.bounds.size.x / 2;
        }
    }


    private void FindNearestPlatform()
    {
        GameObject[] solid_platforms = GameObject.FindGameObjectsWithTag("Platform_Solid");

        GameObject[] mutate_platforms = GameObject.FindGameObjectsWithTag("Platform_Mutate");

        GameObject[] allPlatforms = solid_platforms.Concat(mutate_platforms).ToArray();


        float minDistance = float.MaxValue;

        foreach (GameObject tmpPlatform in allPlatforms)
        {
            float distance = Vector3.Distance(gameObject.transform.position, tmpPlatform.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPlatform = tmpPlatform;
            }
        }
    }
}
