using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int direction = 1; // direction -- 1: right; -1: left

    void Start()
    {
        SceneLoader = GameObject.Find("SceneManager");
        FindNearestPlatform();
        GetBoundary();
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
        GameObject[] allPlatforms = GameObject.FindGameObjectsWithTag("Platform_Solid");

        // // GameObject[] mutate_platforms = GameObject.FindGameObjectsWithTag("Platform_Mutate");

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


    void Update()
    {
        // 获取当前位置
        Vector2 currentPosition = transform.position;
        // Debug.Log("currentPosition: " + currentPosition);

        // // 根据方向和速度更新位置
        currentPosition.x += moveSpeed * direction * Time.deltaTime;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
