using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private GameObject ground; // Reference to the ground object.

    private Collider2D groundCollider;
    private GameObject groundObject;


    private void Awake()
    {
        // Find the GameObject with the "Ground" tag in the current scene.
        groundObject = GameObject.FindGameObjectWithTag("Ground");

        if (groundObject == null)
        {
            Debug.LogError("No GameObject with the 'Ground' tag found in the current scene.");
        }
    }

    private void Start()
    {

        // Find the GameObject with the "ground" tag.
        groundObject = GameObject.FindGameObjectWithTag("Ground");


    }


    private void Update()
    {
        if (IsOutOfBounds())
        {
            // The object is out of bounds, so destroy it.
            Destroy(gameObject);
        }
    }

    private bool IsOutOfBounds()
    {
        // Check if a GameObject with the "ground" tag was found.
        if (groundObject != null)
        {
            groundCollider = groundObject.GetComponent<Collider2D>();

            if (groundCollider == null)
            {
                Debug.LogError("The ground GameObject does not have a Collider2D component.");
            }
            else
            {
                // Calculate the bounds of the ground and the object.
                Bounds groundBounds = groundCollider.bounds;
                Bounds objectBounds = GetComponent<Collider2D>().bounds;

                // Check if the object's horizontal bounds are completely outside the ground's horizontal bounds.
                return objectBounds.max.x < groundBounds.min.x || objectBounds.min.x > groundBounds.max.x;
            }
        }

        return false;
    }
}
