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
    // code for avoiding conflict with Drag and shoot
    private GameObject nearestNumber = null;
    private GameObject[] allNumbers = null;
    private float minDistance = 1.0f;
    private GameObject numberText = null;
    private GameObject playerTextGameObject = null;
    private TMP_Text playerText = null;
    private int number = 0;
    private int playerNumber = 0;
    private int result = 0;
    private GameObject monster = null;


    private void Start()
    {
        // Get the name of the prefab instance by accessing the name property of the GameObject.
        prefabName = gameObject.name;
        nearestNumber = null;
        player = GameObject.Find("Player");
        playerTextGameObject = player.transform.Find("Player_Number").gameObject;
        playerText = playerTextGameObject.GetComponent<TMP_Text>();

    }


    public void Initialize(Vector2 direction)
    {
        // Set the initial velocity to move the bullet.
        GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
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


    private void OnTriggerEnter2D(Collider2D obstacle)
    {
        if (obstacle.gameObject.CompareTag("Gun"))
        { return; }


        if (obstacle.gameObject.CompareTag("Monster2"))
        {
            monster = obstacle.gameObject;
            UpdateMonster2NumberText();
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform_Solid") || other.gameObject.CompareTag("Platform_Mutate") || other.gameObject.CompareTag("Elevator2"))
        {
            Destroy(gameObject);
        }

    }


    private void FindnearestNumber()
    {
        allNumbers = GameObject.FindGameObjectsWithTag("Number");

        if (allNumbers.Length == 0)
        {
            // 如果没有球体对象，不执行任何操作
            return;
        }

        // 计算最近的球体对象
        nearestNumber = null;

        foreach (GameObject number in allNumbers)
        {
            float distance = Vector3.Distance(transform.position, number.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNumber = number;
            }
        }
    }

    private void UpdatenearestNumberText()
    {
        numberText = nearestNumber.gameObject.transform.Find("Number_Text").gameObject;

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

    private void UpdateMonster2NumberText()
    {
        numberText = monster.gameObject.transform.Find("Monster_Text").gameObject;
        Debug.Log("numberText: " + numberText);
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

}