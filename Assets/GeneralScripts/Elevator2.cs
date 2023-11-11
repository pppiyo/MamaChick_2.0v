using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator2 : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.0f; // vertical speed of elevator

    [SerializeField]
    private int condition = 1; // ?
    private GameObject SceneLoader;
    public GameObject lowerPlatform; // Reference to the platform that gives the lower boundary of the elevator
    public GameObject upperPlatform; // Reference to the platform that gives the upper boundary of the elevator
    private float upperBoundary; // y position of the upper boundary
    private float lowerBoundary; // y position of the lower boundary
    private int direction = 1; // up: 1, down: -1 
    private Vector2 currentPosition; // current position of the elevator

    void Start()
    {
        SceneLoader = GameObject.Find("SceneManager");
        GetBoundary();
    }

    // Get the upper and lower double boundaries of the elevator
    private void GetBoundary()
    {
        if (upperBoundary != null || lowerBoundary != null)
        {
            Transform upperPlatformTransform = upperPlatform.transform;
            Transform lowerPlatformTransform = lowerPlatform.transform;

            // Calculate the left and right boundaries of the platform
            upperBoundary = upperPlatformTransform.position.y;
            lowerBoundary = lowerPlatformTransform.position.y;
        }
    }



    // Update is called once per frame
    void Update()
    {
        // Get current position
        currentPosition = transform.position;
        // Debug.Log("currentPosition: " + currentPosition);

        // Update current position with time
        currentPosition.y += moveSpeed * direction * Time.deltaTime;
        transform.position = currentPosition;

        // // 检查是否达到边界，如果是，改变方向
        if (currentPosition.y <= lowerBoundary)
        {
            direction = 1; // face down
        }
        else if (currentPosition.y >= upperBoundary)
        {
            direction = -1; // face up
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Monster2 collided with elevator2");
        // if (collision.gameObject.CompareTag("Monster2"))
        // {
        //     if (collision.gameObject.transform.position.y > transform.position.y)
        //     {
        //         lowerBoundary = collision.gameObject.transform.position.y - collision.gameObject.transform.localScale.y / 2;
        //     }
        //     else
        //     {
        //         upperBoundary = collision.gameObject.transform.position.y + collision.gameObject.transform.localScale.y / 2;

        //     }
        // }
        // Debug.Log("Player collided with elevator");
    }
}