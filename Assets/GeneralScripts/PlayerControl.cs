using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 15;
    private float xBound = 9.88f;
    public float jumpForce = 40;
    private Rigidbody2D playerRB;
    private Vector2 force;
    private bool isGrounded;

    void Start() 
    {
        playerRB = GetComponent<Rigidbody2D>();
        force = jumpForce * Vector2.up;
        isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D obstacle)
    {
        if(obstacle.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D obstacle)
    {
        if (obstacle.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    void Update()
    {
        // Move back and forth
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * Time.deltaTime * (speed) * horizontalInput);

        //  Keep player in bound
        if (transform.position.x > xBound)
            transform.position = new Vector2(xBound, transform.position.y);
        else if (transform.position.x < -xBound)
            transform.position = new Vector2(-xBound, transform.position.y);

        // Jump With Impulse Force 
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
