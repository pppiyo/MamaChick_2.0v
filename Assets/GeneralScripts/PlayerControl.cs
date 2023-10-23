using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 40.0f;
    public float jumpForce = 5.0f;
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
    }
}
