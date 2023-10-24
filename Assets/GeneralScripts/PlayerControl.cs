using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public float horizontalInput;
    public float speed;
    public float jumpForce;
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
        if (obstacle.gameObject.CompareTag("Ground"))
            isGrounded = true;

        // 如果玩家与一个障碍物碰撞
        if (obstacle.gameObject.CompareTag("Ground"))
        {
            Debug.Log("We're here!");
            // foreach (Transform child in obstacle.gameObject.transform)
            // {
            //     Component[] components = child.GetComponents<Component>();
            //     foreach (Component component in components)
            //     {
            //         Debug.Log("Child: " + child.name + ", Component Type: " + component.GetType().ToString());
            //     }
            // }

            TextMeshPro obstacleEquationText = obstacle.gameObject.GetComponentInChildren<TextMeshPro>();
            if (obstacleEquationText != null)
            {
                // Debug.Log("Found TextMeshPro with text: " + obstacleEquationText.text);
            }
            else
            {
                // Debug.Log("TextMeshPro not found!");
            }

            if (obstacleEquationText != null)
            {
                GameObject obj = GameObject.Find("EquationManager");
                
                JudgeEquation judge = obj.GetComponent<JudgeEquation>();

                // 设置JudgeEquation组件的equationText属性为障碍物上的方程
                judge.equationText = obstacleEquationText;

                // 设置JudgeEquation的targetObject为碰撞的障碍物
                judge.targetObject = obstacle.gameObject;

                // 获取玩家上的TextMeshProUGUI组件的值
                GameObject player = GameObject.Find("Player");
                judge.playerEquationText = player.GetComponentInChildren<TextMeshPro>();
                Debug.Log(judge.playerEquationText.text);
                Debug.Log(judge.equationText.text);


                if (judge.playerEquationText != null)
                {
                    judge.varValue = int.Parse(judge.playerEquationText.text);
                }
                
                // 调用EvaluateFromTextMeshPro方法来检查玩家的数值是否满足方程
                judge.EvaluateFromTextMeshPro();
            }
        }
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
