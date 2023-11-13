using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Monster2Movement : MonoBehaviour
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

    private TMP_Text numberText;  // Reference to the TMP_Text component with the number.
    public float scaleMultiplier = 0.1f;  // Adjust this to control the scaling rate.

    private Vector3 initialScale;
    private float initialHeight;
    private Vector3 initialPosition;
    private float yOffset;

    private int MAX = 40;
    private int actualValue = 0;

    // private Vector3 originalSize;

    void Start()
    {
        SceneLoader = GameObject.Find("SceneManager");
        GetnumberText();
        GetInitialPositionPosition();
        leftBoundary = 20.0f;
        rightBoundary = 30.0f;
    }


    private void GetInitialPositionPosition()
    {
        // Record the initial scale, height, and position of the GameObject.
        initialScale = transform.localScale;
        initialHeight = numberText.bounds.size.y;
        initialPosition = transform.position;
    }

    private void GetnumberText()
    {
        numberText = gameObject.transform.Find("Monster_Text").GetComponent<TMP_Text>();

        // Ensure you have a reference to the Text component.
        if (numberText == null)
        {
            Debug.LogError("Number Text component is missing.");
            return;
        }
    }


    void Update()
    {
        UpdateSize();
        UpdateMovement();

    }

    private void UpdateSize()
    {
        if (int.TryParse(numberText.text, out int number))
        {
            if (number <= 0)
            {
                Destroy(gameObject);
            }
            else if (number > MAX)
            {
                number = MAX;
            }

            actualValue = number;
            // Calculate the new scale based on the number for both width and height.
            float scaleFactor = number * scaleMultiplier;

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
        else
        {
            Debug.LogError("Failed to parse the number.");
        }
    }

    private void UpdateMovement()
    {
        if (actualValue == MAX)
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
        // 获取当前位置

    }


    // private void OnCollisionEnter2D(Collision2D other)
    private void OnTriggerEnter2D(Collider2D other)
    {
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

        // if (other.gameObject.CompareTag("Elevator2"))
    }

    // private void GetBoundary()
    // {
    //     if (nearestPlatform != null)
    //     {
    //         Transform platformTransform = nearestPlatform.transform;

    //         Renderer platformRenderer = nearestPlatform.GetComponent<Renderer>();
    //         Renderer monsterRenderer = gameObject.GetComponent<Renderer>();

    //         // Calculate the left and right boundaries of the platform
    //         leftBoundary = platformTransform.position.x - platformRenderer.bounds.size.x / 2 + monsterRenderer.bounds.size.x / 2;
    //         rightBoundary = platformTransform.position.x + platformRenderer.bounds.size.x / 2 - monsterRenderer.bounds.size.x / 2;
    //     }
    // }

}
