using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Elevator : MonoBehaviour
{
    public float targetHeight;
    
    public float moveSpeed;

    public int direction; //1:up, 2: down

    private bool isMoving = false;

    private Collision2D ball;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ball = collision;
        if ((collision.gameObject.CompareTag("Number")) && !isMoving)
        {
            // 如果碰到的物体的标签是"number" 且电梯没有在移动中
            // 在这里可以添加你的电梯升高的代码
            if(CanTakeElevator(gameObject))
                StartCoroutine(MoveElevator());
            Destroy(collision.gameObject);
            Debug.Log(CanTakeElevator(gameObject));
        }
    }

    private IEnumerator MoveElevator()  
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        // float targetHeight = 8f; // 电梯目标高度
        // float moveSpeed = 2f; // 升降速度

        if(direction == 1){
            while (transform.position.y < targetHeight)
            {
                // 在这里更新电梯的高度
                Vector3 newPosition = transform.position + Vector3.up * moveSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, Mathf.Min(newPosition.y, targetHeight), transform.position.z);
                yield return null;
            }
        }

        else if(direction == 2){
            while (transform.position.y > targetHeight)
            {
                // 在这里更新电梯的高度
                Vector3 newPosition = transform.position + Vector3.down * moveSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, Mathf.Max(newPosition.y, targetHeight), transform.position.z);
                yield return null;
            }
        }
         yield return new WaitForSeconds(5f);
    // Debug.Log(startPosition);

    // back to original place
        if (direction == 1)
        {
            while (transform.position.y > startPosition.y)
            {
                Vector3 newPosition = transform.position + Vector3.down * moveSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, Mathf.Max(newPosition.y, startPosition.y), transform.position.z);
                yield return null;
            }
        }
        else if (direction == 2)
        {
            while (transform.position.y < startPosition.y)
            {
                Vector3 newPosition = transform.position + Vector3.up * moveSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, Mathf.Min(newPosition.y, startPosition.y), transform.position.z);
                yield return null;
            }
        }
        

        isMoving = false;
    }

        bool CanTakeElevator(GameObject obstacle)
        {
            bool pass = false;

            // Platform Equation Logic
            TextMeshPro obstacleEquationText = obstacle.GetComponentInChildren<TextMeshPro>();
            if (obstacleEquationText != null && obstacleEquationText.text.Length != 0)
            {
                GameObject obj = GameObject.Find("EquationManager");

                JudgeEquation judge = obj.GetComponent<JudgeEquation>();

                // 设置JudgeEquation组件的equationText属性为障碍物上的方程
                judge.equationText = obstacleEquationText;
                
                // 设置JudgeEquation的targetObject为碰撞的障碍物
                judge.targetObject = obstacle.gameObject;

                
                judge.playerEquationText = ball.gameObject.GetComponentInChildren<TextMeshPro>();
                // Debug.Log(judge.playerEquationText.text);

                if (judge.playerEquationText != null)
                {
                    Debug.Log(obstacleEquationText.text);
                    judge.varValue = int.Parse(judge.playerEquationText.text);
                }
                // Debug.Log(judge.varValue);

                // 调用EvaluateFromTextMeshPro方法来检查玩家的数值是否满足方程
                pass = judge.EvaluateFromTextMeshPro(); // if 
            }
            return pass;
        }


    
}
