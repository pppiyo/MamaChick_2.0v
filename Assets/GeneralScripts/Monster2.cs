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

    public TMP_Text numberText;  // Reference to the TMP_Text component with the number.
    public float scaleMultiplier = 0.1f;  // Adjust this to control the scaling rate.

    private Vector3 initialScale;
    private float initialHeight;
    private Vector3 initialPosition;
    private float yOffset;




    // private TMP_Text numberText;
    // private string monsterText;
    // // private TMP_Text textComponent;

    // private int currentNumber;
    // [SerializeField] private float scaleMultiplier = 0.02f;  // Adjust this to control the scaling rate.

    // private float originalBottom;
    // private Vector3 originalSize;

    void Start()
    {
        SceneLoader = GameObject.Find("SceneManager");
        GetnumberText();
        GetInitialPositionPosition();
        FindNearestPlatform();
        GetBoundary();
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
        // numberTextTMP = gameObject.transform.Find("Number_Text").gameObject.GetComponent<TMP_Text>();

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
        // GetComponent<TMP_Text>().fontSize = currentNumber * scaleMultiplier;

        // Parse the number from the TMP_Text component.
        if (int.TryParse(numberText.text, out int number))
        {
            // Calculate the new scale factor based on the number.
            float scaleFactor = number * scaleMultiplier;

            // Update the GameObject's scale.
            transform.localScale = new Vector3(initialScale.x + scaleFactor, initialScale.y + scaleFactor, initialScale.z);

            // Calculate the position adjustment to maintain the bottom on the ground.
            float newHeight = initialHeight * scaleFactor;
            yOffset = (newHeight - initialHeight) / 2.0f;

            // Adjust the GameObject's position to keep the bottom on the ground.
            // transform.position = new Vector3(initialPosition.x, initialPosition.y + yOffset, initialPosition.z);
        }
        else
        {
            Debug.LogError("Failed to parse the number.");
        }

    }

    private void UpdateMovement()
    {
        // 获取当前位置
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
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
