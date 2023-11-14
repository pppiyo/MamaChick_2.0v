using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f; // 移动速度
    public float leftBoundary = -2.5f; // 左边界
    public float rightBoundary = 2.5f; // 右边界
    public int condition = 1;
    private GameObject SceneLoader;
    private int direction = 1; // 初始方向为向右
    private GameObject checkTutorial;
    public List<GameObject> prefabList;

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
                        GlobalVariables.monsterKilled++;
                        dropRandomObject();
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                        killedByMonster(gameObject);
                    }
                    break; // 如果不使用break语句，将继续执行下一个case（如果条件匹配）。
                case 2:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX % 2 == 1)
                    {
                        Destroy(gameObject);
                        GlobalVariables.monsterKilled++;
                        dropRandomObject();
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                        killedByMonster(gameObject);
                    }
                    break;
                case 3:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX % 2 == 0)
                    {
                        Destroy(gameObject);
                        GlobalVariables.monsterKilled++;
                        dropRandomObject();
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                        killedByMonster(gameObject);
                    }
                    break;
                case 4:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX < 10)
                    {
                        Destroy(gameObject);
                        GlobalVariables.monsterKilled++;
                        dropRandomObject();
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                        killedByMonster(gameObject);
                    }
                    break;
                case 5:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX < 0)
                    {
                        Destroy(gameObject);
                        GlobalVariables.monsterKilled++;
                        dropRandomObject();
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                        killedByMonster(gameObject);
                    }
                    break;
                case 6:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX > 5)
                    {
                        Destroy(gameObject);
                        GlobalVariables.monsterKilled++;
                        dropRandomObject();
                    }
                    else
                    {
                        if (checkTutorial == null)
                        {
                            Destroy(collision.gameObject);
                            killedByMonster(gameObject);
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
                        GlobalVariables.monsterKilled++;
                        dropRandomObject();
                    }
                    else
                    {
                        if (checkTutorial == null)
                        {
                            killedByMonster(gameObject);
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
                        GlobalVariables.monsterKilled++;
                        dropRandomObject();
                    }
                    else
                    {
                        if (checkTutorial == null)
                        {
                            killedByMonster(gameObject);
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
                        GlobalVariables.monsterKilled++;
                        dropRandomObject();
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                        killedByMonster(gameObject);
                    }
                    break;
                case -2:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX > 15)
                    {
                        Destroy(gameObject);
                        GlobalVariables.monsterKilled++;
                        dropRandomObject();
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                        killedByMonster(gameObject);

                    }
                    break;
                case -3:
                    // 当expression等于value2时执行的代码
                    if (playerControl.currentX < 3)
                    {
                        Destroy(gameObject);
                        GlobalVariables.monsterKilled++;
                        dropRandomObject();
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                        killedByMonster(gameObject);

                    }
                    break;
                default:
                    // 当expression与任何case都不匹配时执行的默认代码块
                    break;
            }
        }
    }
    
    private void killedByMonster(GameObject gameObject)
    {
        GlobalVariables.failReason = "killedByMonster " + gameObject.name;
        SceneLoader.GetComponent<Transition>().LoadGameOverLost();
    }

    private void dropRandomObject(){
            if (prefabList.Count > 0)
                {
                    // Select and instantiate a random prefab at the current box's position
                    GameObject selectedPrefab = prefabList[Random.Range(0, prefabList.Count)];
                    GameObject newPrefabInstance = Instantiate(selectedPrefab, transform.position, Quaternion.identity);

                    // Check if the selected prefab is named 'number'
                    if (newPrefabInstance.name.Contains("Number"))
                    {
                        // Find the TMP component and assign a random number between 1-10
                        TMP_Text numberText = newPrefabInstance.transform.Find("Number_Text")?.GetComponent<TMP_Text>();
                        if (numberText != null)
                        {
                            numberText.text = Random.Range(1, 11).ToString();
                        }
                    }
                }
    }
}
