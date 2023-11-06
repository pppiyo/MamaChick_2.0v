using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f; // 移动速度
    public float leftBoundary = -2.5f; // 左边界
    public float rightBoundary = 2.5f; // 右边界
    public int condition = 1;
    private GameObject SceneLoader;
    private int direction = 1; // 初始方向为向右
    private GameObject checkTutorial;

    void Start()
    {
        SceneLoader = GameObject.Find("SceneManager");
        checkTutorial = GameObject.Find("TutorialInstructions");
    }

    void Update()
    {
        // 获取当前位置
        Vector2 currentPosition = transform.position;

        // 根据方向和速度更新位置
        currentPosition.x += moveSpeed * direction * Time.deltaTime;
        transform.position = currentPosition;

        // 检查是否达到边界，如果是，改变方向
        if (currentPosition.x <= leftBoundary)
        {
            direction = 1; // 向右
        }
        else if (currentPosition.x >= rightBoundary)
        {
            direction = -1; // 向左
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControl playerControl = collision.gameObject.GetComponent<PlayerControl>();
            switch (condition)
            {
                case 1:
                    // 当expression等于value1时执行的代码
                    if (playerControl.currentX < 15)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }
                    break; // 如果不使用break语句，将继续执行下一个case（如果条件匹配）。
                case 2:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX % 2 == 1)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }
                    break;
                case 3:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX % 2 == 0)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }
                    break;
                case 4:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX < 10)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }
                    break;
                case 5:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX < 0)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }
                    break;
                case 6:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX > 5)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        if (checkTutorial == null)
                        {
                            Destroy(collision.gameObject);
                        }
                        else
                        {
                            if (GlobalVariables.curLevel == "tutorial 2")
                            {
                                GameObject.Find("Player").transform.position = GameObject.Find("Checkpoint2").transform.position;
                            }
                        }
                    }
                    break;
                case 7:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX > 200)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        if (checkTutorial == null)
                        {
                            Destroy(collision.gameObject);
                        }
                        else
                        {
                            if (GlobalVariables.curLevel == "tutorial 2")
                            {
                                GameObject.Find("Player").transform.position = GameObject.Find("Checkpoint2").transform.position;
                            }
                        }
                    }
                    break;
                case 8:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX > 100)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        if (checkTutorial == null)
                        {
                            Destroy(collision.gameObject);
                        }
                        else
                        {
                            if (GlobalVariables.curLevel == "tutorial 2")
                            {
                                GameObject.Find("Player").transform.position = GameObject.Find("Checkpoint2").transform.position;
                            }
                        }
                    }
                    break;
                    
                case -1:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX > 10)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                        SceneLoader.GetComponent<Transition>().LoadGameOverLost();
                    }
                    break;
                case -2:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX > 15)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                        SceneLoader.GetComponent<Transition>().LoadGameOverLost();

                    }
                    break;
                case -3:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX < 3)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                        SceneLoader.GetComponent<Transition>().LoadGameOverLost();

                    }
                    break;
                default:
                    // 当expression与任何case都不匹配时执行的默认代码块
                    break;
            }
        }
    }
}
