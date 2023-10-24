using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public float horizontalInput;
    public float speed;
    public float jumpForce;
    public int currentX;
    public GameObject xObject;
    public TMP_Text xBoard;
    public GameObject WheelManager;
    public int operatorID;
    public float xBound;
    private Rigidbody2D playerRB;
    private Vector2 force;
    private int increaseX;
    private bool isGrounded;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        force = jumpForce * Vector2.up;
        isGrounded = false;
        currentX = 0;
        xBoard = xObject.GetComponent<TMP_Text>();
        operatorID = 0;
    }

    void Update()
    {
        // Move back and forth
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * Time.deltaTime * (speed) * horizontalInput);

        //  Keep player in bound
        if (transform.position.x > xBound)
            transform.position = new Vector2(xBound, transform.position.y);
        else if (transform.position.x < -xBound)
            transform.position = new Vector2(-xBound, transform.position.y);

        // Jump With Impulse Force 
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.AddForce(force, ForceMode2D.Impulse);
        }

        // Operator Update 
        operatorID = WheelManager.GetComponent<WheelController>().operatorID;
    }

    void OnCollisionEnter2D(Collision2D obstacle)
    {
        // Score update
        if (obstacle.gameObject.CompareTag("Number"))
        {
            increaseX = int.Parse(Regex.Match(obstacle.gameObject.name, @"\d+$").Value);
            switch (operatorID)
            {
                case 0:
                    currentX += increaseX;
                    break;
                case 1:
                    currentX -= increaseX;
                    break;
                case 2:
                    currentX *= increaseX;
                    break;
                case 3:
                    currentX /= increaseX;
                    break;
            }
            Destroy(obstacle.gameObject);
            xBoard.text = currentX.ToString();
        }

        // 如果玩家与一个障碍物碰撞
        if (obstacle.gameObject.CompareTag("Ground"))
        {
            // Jump Enabled
            isGrounded = true;

            // Platform Equation Logic
            TextMeshPro obstacleEquationText = obstacle.gameObject.GetComponentInChildren<TextMeshPro>();
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

        void OnCollisionExit2D(Collision2D obstacle)
        {
            // Jump Disabled
            if (obstacle.gameObject.CompareTag("Ground"))
                isGrounded = false;

        }
    }
}