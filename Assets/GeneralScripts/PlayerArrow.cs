using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    private Transform player; // 玩家的Transform
    private Transform target; // 目标的Transform
    private float distanceFromPlayer = 1.5f; // 箭头距离玩家的距离

    void Start()
    {
        // 通过标签找到玩家和目标对象
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Goal").transform;
    }
    
    void Update()
    {
        if (target != null && player != null)
        {
            // 计算从玩家到目标的2D方向
            Vector2 direction = (target.position - player.position).normalized;

            // 设置箭头的位置在玩家和目标之间的连线上，距离玩家固定距离
            transform.position = (Vector2)player.position + direction * distanceFromPlayer;

            // 计算箭头指向目标的2D旋转角度
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 调整角度以使箭头的尖部分指向目标
            // 假设箭头Sprite的尖部分初始是向上的
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }
    }
}






