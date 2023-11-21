using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private Vector3 originalPosition;
    void Start()
    {
        originalPosition = transform.position;
    }

    void LateUpdate()
    {
        transform.position = originalPosition;
    }
}
