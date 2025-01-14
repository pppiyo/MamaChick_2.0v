using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Number : MonoBehaviour
{
    public float respawnDelay = 3f; // 重生的延迟时间
    public int maxRespawns = 1; // 最大重生次数
    public bool isRespawnable = false; // 是否可重生
    private Vector3 initialPosition; // 初始位置
    private int respawnCount = 0; // 当前重生次数


    void Start()
    {
        initialPosition = transform.position; // 缓存游戏开始时的位置
    }

    public void Respawn()
    {
        if (!isRespawnable)
        {
            Destroy(gameObject);
            return; // 如果不可重生，直接返回
        }
        else
        {
            if (respawnCount < maxRespawns)
            {
                gameObject.SetActive(false); // 禁用对象
                Invoke(nameof(ResetPosition), respawnDelay); // 延迟调用以重生对象
                respawnCount++; // 增加重生次数
            }
            else
            {
                Destroy(gameObject); // 达到最大重生次数，销毁对象
            }

        }
    }

    private void ResetPosition()
    {
        // tweak the position to avoid overlapping with the player
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            float playerWidth = player.transform.localScale.x;
            float numberWidth = transform.localScale.x;
            // float margin = playerWidth / 6.0;
            // float offset = (playerWidth + numberWidth) / 2 + margin;
            float offset = (playerWidth + numberWidth) / 2;
            if (Math.Abs(initialPosition.x - player.transform.position.x) < offset)
            {
                if (initialPosition.x < player.transform.position.x)
                    initialPosition.x -= offset;
                else
                    initialPosition.x += offset;
            };
        }
        transform.position = initialPosition; // 重置位置到初始位置
        gameObject.SetActive(true); // 重新激活对象
    }

}

