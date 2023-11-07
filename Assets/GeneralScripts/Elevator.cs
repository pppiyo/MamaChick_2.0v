using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool isMoving = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Goal");
        if ((collision.gameObject.CompareTag("Number")) && !isMoving)
        {
            // 如果碰到的物体的标签是"number" 且电梯没有在移动中
            // 在这里可以添加你的电梯升高的代码
            StartCoroutine(MoveElevator());
        }
    }

    private IEnumerator MoveElevator()
    {
        isMoving = true;
        float targetHeight = 8f; // 电梯目标高度
        float moveSpeed = 2f; // 升降速度

        while (transform.position.y < targetHeight)
        {
            // 在这里更新电梯的高度
            Vector3 newPosition = transform.position + Vector3.up * moveSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, Mathf.Min(newPosition.y, targetHeight), transform.position.z);
            yield return null;
        }

        isMoving = false;
    }


    
}
