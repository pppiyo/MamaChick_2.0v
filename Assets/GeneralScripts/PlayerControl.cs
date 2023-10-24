using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float horizontalInput;
    public float speed;
    public float jumpForce;
    public int currentX;
    public GameObject xObject; 
    public TMP_Text xBoard;
    public GameObject WheelManager;
    public int operatorID;
    private Rigidbody2D playerRB;
    private Vector2 force;
    private int increaseX; 
    private bool isGrounded;

    void Start() 
    {
        playerRB = GetComponent<Rigidbody2D>();
        force = jumpForce * Vector2.up;
        isGrounded = false;
        currentX = 0;
        xBoard = xObject.GetComponent<TMP_Text>();
        operatorID = 0;
    }

    void OnCollisionEnter2D(Collision2D obstacle)
    {
        // Jump Enabled
        if(obstacle.gameObject.CompareTag("Ground"))
            isGrounded = true;
        // Score update
        if (obstacle.gameObject.CompareTag("Number"))
        {
            increaseX = int.Parse(Regex.Match(obstacle.gameObject.name, @"\d+$").Value);
            switch (operatorID)
            {
                case 0:
                    currentX += increaseX;
                    break;
                case 1:
                    currentX -= increaseX;
                    break;
                case 2:
                    currentX *= increaseX;
                    break;
                case 3:
                    currentX /= increaseX;
                    break;
            }
            Destroy(obstacle.gameObject);
            xBoard.text = currentX.ToString();
        }
    }

    void OnCollisionExit2D(Collision2D obstacle)
    {
        // Jump Disabled
        if (obstacle.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    // todo: when grab worm and pebble, make them use gravity. // currently, they are kinematic and don't use gravity.
    void Update()
    {
        // Move back and forth
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * Time.deltaTime * (speed) * horizontalInput);

        // Jump With Impulse Force 
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.AddForce(force, ForceMode2D.Impulse);
        }

        // Operator Update 
        operatorID = WheelManager.GetComponent<WheelController>().operatorID;
    }
}
