using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTracker : MonoBehaviour
{
    public float yLowerBoundary;
    public float cameraSmoothing;
    private int initialSet;
    private GameObject player;
    private Vector3 offset;
    private Vector3 targetPosition;
    private Vector3 xPosition;
    private Vector3 yPosition;

    void Start()
    {
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;
        initialSet = 0;
        Debug.Log(player.transform.position);
        Debug.Log(offset);
    }


    // Update is called once per frame
    void Update()
    {
        switch (initialSet)
        {
            case 0:
                if(player.transform.position.x > 0)
                {
                    offset = transform.position - player.transform.position;
                    initialSet = 1;
                }
                break;
            case 1:
                targetPosition = offset + player.transform.position;
                xPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
                if (player.GetComponent<Rigidbody2D>().velocity.y < 5)
                {
                    transform.position = Vector3.Lerp(transform.position, xPosition, cameraSmoothing * Time.deltaTime);
                    offset = transform.position - player.transform.position;
                }
                else
                    transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSmoothing * Time.deltaTime);
                break;
            case 2:
                break;
        }
    }
}
