using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            {
                Debug.Log("Checkpoint reached");

                // Get the child GameObject (assuming it's the first child)
                Transform childTransform = transform.GetChild(0).transform.GetChild(1);

                // Get the SpriteRenderer component on the child GameObject
                SpriteRenderer childSpriteRenderer = childTransform.GetComponent<SpriteRenderer>();

                // Change the color to blue
                childSpriteRenderer.color = Color.green;
            }

        }
    }
}
