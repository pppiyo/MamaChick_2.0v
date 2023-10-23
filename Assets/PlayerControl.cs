using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
    }
    void HorizontalMove()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * speed * Time.deltaTime;
        if (horizontalInput !=0)
        {
            moveDirection = new Vector3(horizontalInput, 0f, 0f);
            moveDirection = moveDirection.normalized;
        }
        transform.Translate(movement);
        
    }
}
