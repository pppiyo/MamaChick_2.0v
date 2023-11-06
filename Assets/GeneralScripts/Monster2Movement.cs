using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f; // 移动速度
    private float leftBoundary; // 左边界
    private float rightBoundary; // 右边界
    [SerializeField]
    private int condition = 1;
    private GameObject SceneLoader;
    private GameObject nearestPlatform; // Reference to the platform the monster is on
    private GameObject[] allPlatforms;


    private int direction = 1; // 初始方向为向右

    void Start()
    {
        SceneLoader = GameObject.Find("SceneManager");
        FindNearestPlatform();
        GetBoundary();
    }

    private void GetBoundary()
    {
        if (nearestPlatform == !null)
        {
            Transform platformTransform = nearestPlatform.transform;
            // Debug.Log("platformTransform: " + platformTransform.transform);
            Renderer platformRenderer = nearestPlatform.GetComponent<Renderer>();

            Vector3 monster2Size = renderer.bounds.size;

            // Calculate the left and right boundaries of the platform
            leftBoundary = platformTransform.position.x - platformRenderer.bounds.size.x / 2;
            // leftBoundary = platformTransform.position.x - platformRenderer.bounds.size.x / 2 + monster2Size.x / 2;
            rightBoundary = platformTransform.position.x + platformRenderer.bounds.size.x / 2;
            // rightBoundary = platformTransform.position.x + platformRenderer.bounds.size.x / 2 - monster2Size.x / 2;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControl playerControl = collision.gameObject.GetComponent<PlayerControl>();
            switch (condition)
            {
                case 1:
                    // 当expression等于value1时执行的代码
                    if (playerControl.currentX < 15)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }
                    break; // 如果不使用break语句，将继续执行下一个case（如果条件匹配）。
                case 2:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX % 2 == 1)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }
                    break;
                case 3:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX % 2 == 0)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }
                    break;
                case 4:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX < 10)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }
                    break;
                case 5:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX < 0)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }
                    break;

                default:
                    // 当expression与任何case都不匹配时执行的默认代码块
                    break;
            }
        }
    }
}
