using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class PlayerControl : MonoBehaviour
{
    private bool facingRight = true; // To keep track of the player's facing direction.
    public float horizontalInput;
    public float speed;
    public float jumpForce;
    public int currentX;
    public GameObject xObject;
    private TMP_Text xBoard;
    public GameObject WheelManager;
    public int operatorID; // 0: add; 1: sub; 2: multiply; 3: divide; 4: no operator selected
    public int gravityDirection;
    public TMP_Text hintText;
    public GameObject SceneLoader;
    private Rigidbody2D playerRB;
    private Vector2 force;
    private int increaseX;
    public bool isGrounded;
    private Vector2 invertedGravity;
    private int previousResult;
    private string hintDisplay;
    private JudgeEquation judgeEquation;
    private LayerMask playerLayer;
    private LayerMask platformLayer;
    private float effectDuration = 0.8f;
    private Transform nearbyTeleporterDestination;
    private List<GameObject> platforms;
    private Vector3 moveDirection;
    private GameObject tutorialCheck;
    private GameObject numberTextGameObject;
    private string numberText;
    private Collider2D currentCollidingObject = null;
    //test_ball
    public static GameObject nearestBall;
    private bool canTeleport = true;


    // Parameters to keep the player in bound
    public Transform ground; // Reference to the ground object.
    private Collider2D playerCollider;
    private Collider2D groundCollider;
    public float rightBound;
    public float leftBound;


    void Start()
    {

        // Keep the player in bound
        SetPlayerBoundary();
        //KeepPlayerInBound();

        // 查找所有包含 "platform" 字符串的游戏对象
        GameObject[] platformObjects = FindObjectsWithSubstring("Platform");
        gravityDirection = 1;
        invertedGravity = new Vector2(0, -9.81f);

        // 输出找到的游戏对象的名称
        foreach (GameObject obj in platformObjects)
        {
            GlobalVariables.platformMap[obj.name] = 0;
        }
        playerRB = GetComponent<Rigidbody2D>();
        force = jumpForce * Vector2.up;
        isGrounded = false;
        currentX = 0;
        xBoard = xObject.GetComponent<TMP_Text>();
        hintText.gameObject.SetActive(false);
        operatorID = 4;

        // Note: layer is different from tag, where we only use "Platform" and "Fake" to differentiate the valid/invalid platforms, and in tag we simply use Platform_Mutate and Platform_Solid to differentiate the platforms that can be mutated and the platforms that cannot be mutated.
        playerLayer = LayerMask.NameToLayer("Player");
        platformLayer = LayerMask.NameToLayer("Platform");

        platforms = new List<GameObject>();
        // These are the platforms that change dynamically
        platforms.AddRange(GameObject.FindGameObjectsWithTag("Platform_Mutate"));
        platforms.AddRange(GameObject.FindGameObjectsWithTag("Fake"));
        resolvePlatforms();

        // Scene Loader 
        SceneLoader = GameObject.Find("SceneManager");
        // Check if current level is a tutorial scene
        tutorialCheck = GameObject.Find("TutorialInstructions");
    }

    void Update()
    {

        KeepPlayerInBound();

        if (currentCollidingObject != null && Input.GetKeyDown(KeyCode.E))
        {
            // Grab the Number object's text
            numberTextGameObject = currentCollidingObject.gameObject.transform.Find("Number_Text").gameObject;
            numberText = numberTextGameObject.GetComponent<TMP_Text>().text;
            int increaseX = int.Parse(numberText); // number for the value on the Number object
            // Debug.Log(increaseX);
            
            // Use Regex.IsMatch to check if the text in the GameObject in currentCollidingObject contains a number
            if (Regex.IsMatch(numberText, @"\d"))
            // if (Regex.IsMatch(currentCollidingObject.gameObject.name, @"\d"))
            {
                UpdateScore(increaseX);
                resolvePlatforms();
                // Debug.Log(currentX);

                // Debug.Log(GameObject.FindGameObjectsWithTag("Ground"));
                if (tutorialCheck == null)
                    Destroy(currentCollidingObject.gameObject);
                else if (GlobalVariables.curLevel == "tutorial 2" && operatorID != 4)
                {
                    currentCollidingObject.gameObject.SetActive(false);
                }
                currentCollidingObject = null; // Clear the collider reference after processing
            }
        }
        // Detect player input for horizontal movement.
        float horizontalInput = Input.GetAxis("Horizontal");

        // Check if the player is changing direction.
        if ((horizontalInput < 0 && facingRight) || (horizontalInput > 0 && !facingRight))
        {
            // Flip the player's direction.
            Flip();
        }

        // gain the moveDirection
        if (horizontalInput != 0)
        {
            moveDirection = new Vector3(horizontalInput, 0f, 0f);
            moveDirection = moveDirection.normalized;
        }

        // Move back and forth
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            transform.Translate(Vector2.right * Time.deltaTime * (speed) * horizontalInput);

        // //  Keep player in bound
        // if (transform.position.x > rightBound)
        //     transform.position = new Vector2(rightBound, transform.position.y);
        // else if (transform.position.x < leftBound)
        //     transform.position = new Vector2(leftBound, transform.position.y);

        // Jump With Impulse Force 
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            if (gravityDirection == 1)
                playerRB.AddForce(force, ForceMode2D.Impulse);
            else
                playerRB.AddForce((float)-0.7 * force, ForceMode2D.Impulse);
        }

        // Operator Update 
        if (WheelManager != null)
        {
            operatorID = WheelManager.GetComponent<WheelController>().operatorID;
        }

        //press 'E' to teleport
        // if (nearbyTeleporterDestination != null && Input.GetKeyDown(KeyCode.E))
        // {
        //     transform.position = nearbyTeleporterDestination.position;
        // }

        // Flip Gravity
        if (currentX >= 0)
            Physics2D.gravity = invertedGravity;
        else
            Physics2D.gravity = (float)-0.5 * invertedGravity;

        // Change Operator from arrow keys
        if (Input.GetKey(KeyCode.RightArrow))
        {
            operatorID = 1;
            EventSystem.current.SetSelectedGameObject(GameObject.Find("SubButton"));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            operatorID = 3;
            EventSystem.current.SetSelectedGameObject(GameObject.Find("DivButton"));
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            operatorID = 0;
            EventSystem.current.SetSelectedGameObject(GameObject.Find("AddButton"));
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            operatorID = 2;
            EventSystem.current.SetSelectedGameObject(GameObject.Find("MulButton"));
        }

        //test_ball
        if (Input.GetKeyDown(KeyCode.P))
        {
            PickupNearestNumber();
        }

        if (nearestBall != null)
        {
            // 获取玩家头部位置
            Vector3 playerHeadPosition = transform.position + new Vector3(0, 2, 0); // 假设头部相对于玩家位置的偏移是 (0, 2, 0)

            // 设置球体位置为玩家头部位置
            nearestBall.transform.position = playerHeadPosition;

        }

    }

    // Sets the platform logic at start and whenever currentX changes
    public void resolvePlatforms()
    {
        // Check all platforms and separate solid platforms
        // Debug.Log("Resolving platforms");
        foreach (GameObject platform in platforms)
        {
            if (CanPassPlatform(platform))
            {
                DisableLayerCollision(platform);
            }
            else
            {
                EnableLayerCollision(platform);
            }
        }
    }


    private void Flip()
    {
        // Find the "Gun" child of the player GameObject.
        Transform gunChild = transform.Find("Gun");

        if (gunChild != null)
        {
            // You have found the "Gun" child. You can access and manipulate it here.
            // Invert the local scale's x value to flip the player.
            Vector3 newScale = gunChild.transform.localScale;
            newScale.x *= -1;
            gunChild.transform.localScale = newScale;
            gunChild.transform.localPosition = new Vector3(-gunChild.transform.localPosition.x, gunChild.transform.localPosition.y, gunChild.transform.localPosition.z);

            // Update the facing direction flag.
            facingRight = !facingRight;
        }
    }

    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }


    public void ShowHint(string hint)
    {
        hintText.text = hint;
        hintText.gameObject.SetActive(true);
    }

    public IEnumerator HideHint(int delay)
    {
        yield return new WaitForSeconds(delay);
        hintText.gameObject.SetActive(false);
    }

    bool negativeX(int result, int increment)
    {
        previousResult = result;
        hintDisplay = "";
        switch (operatorID)
        {
            case 0:
                result += increment;
                hintDisplay = "Adding " + increment;
                break;
            case 1:
                result -= increment;
                hintDisplay = "Subracting " + increment;
                break;
            case 2:
                result *= increment;
                hintDisplay = "Multiplying " + increment;
                break;
            case 3:
                if (increment != 0)
                {
                    result /= increment;
                    hintDisplay = "Dividing " + increment;
                }
                // else
                // {
                //     hintDisplay = "Cannot divide by 0";
                // }
                break;
        }

        if (result < 0)
        {
            // Flip Gravity Logic;
            if (previousResult >= 0)
            {
                hintDisplay += "\n Flipping Gravity!";
                gravityDirection = gravityDirection * -1;
                ShowHint(hintDisplay);
            }
            else
            {
                ShowHint(hintDisplay);
            }
        }
        else
        {
            // Flip Gravity Logic;
            if (previousResult < 0)
            {
                hintDisplay += "\n Flipping Gravity!";
                gravityDirection = gravityDirection * -1;
                ShowHint(hintDisplay);
            }
            else
            {
                ShowHint(hintDisplay);
            }
        }

        StartCoroutine(HideHint(1));

        return false;
    }

    GameObject[] FindObjectsWithSubstring(string substring)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        List<GameObject> matchingObjects = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains(substring))
            {
                matchingObjects.Add(obj);
            }
        }

        return matchingObjects.ToArray();
    }

    private IEnumerator TeleportCooldown()
    {
        canTeleport = false; // 禁止传送
        yield return new WaitForSeconds(2); // 等待1秒
        canTeleport = true; // 重新启用传送
    }

    void OnCollisionEnter2D(Collision2D obstacle)
    {
        if (obstacle.gameObject.CompareTag("Portal") && canTeleport)
        {
            TextMeshPro portalEquationText = obstacle.gameObject.GetComponentInChildren<TextMeshPro>();
            GameObject obj = GameObject.Find("EquationManager");
            JudgeEquation judge = obj.GetComponent<JudgeEquation>();
            TextMeshPro playerEquationText = GetComponentInChildren<TextMeshPro>();

            bool shouldTeleport = portalEquationText == null || portalEquationText.text.Length == 0 ||
                                (int.TryParse(playerEquationText.text, out int playerNumber) &&
                                judge.CheckEquation(portalEquationText.text, playerNumber));

            if (shouldTeleport)
            {
                string pairName = obstacle.gameObject.name.EndsWith("1") ?
                                obstacle.gameObject.name.Replace("1", "2") :
                                obstacle.gameObject.name.Replace("2", "1");
                GameObject pairPortal = GameObject.Find(pairName);
                if (pairPortal != null)
                {
                    transform.position = pairPortal.transform.position; // Teleport the player
                    ShowHint("Teleported!"); // Show confirmation hint
                    StartCoroutine(TeleportCooldown()); // 开始冷却计时
                }
            }
            else
            {
                ShowHint("Your point doesn't satisfy the condition");
            }

            StartCoroutine(HideHint(1)); // Hide the hint after a delay
        }
        // Tutorial You do not kill anyone
        if (obstacle.gameObject.CompareTag("Spike") && tutorialCheck == null)
        {
            SceneLoader.GetComponent<Transition>().LoadGameOverLost();
        }


        // 如果玩家与一个障碍物碰撞
        if (obstacle.gameObject.CompareTag("Ground") || obstacle.gameObject.CompareTag("Destination"))
        {
            // Jump Enabled
            isGrounded = true;
        }


        // Score update
        if (obstacle.gameObject.CompareTag("Number"))
        {
            increaseX = int.Parse(Regex.Match(obstacle.gameObject.name, @"\d+$").Value); // number for the value on the Number object
            UpdateScore(increaseX);
            resolvePlatforms();
            if (tutorialCheck == null)
                Destroy(obstacle.gameObject);
            if(GlobalVariables.curLevel == "tutorial 2" && operatorID != 4)
            {
                obstacle.gameObject.SetActive(false);
            }
        }

        if (obstacle.gameObject.CompareTag("Goal"))
        {
            // Debug.Log("Goal");
            GlobalVariables.win = true;
            ReturnToMainMenu();
        }
        // if Player collides with a mutate platform
        if (obstacle.gameObject.CompareTag("Platform_Mutate"))
        {
            isGrounded = true;
        }


        // if Player collides with a mutate platform
        if (obstacle.gameObject.CompareTag("Platform_Solid"))
        {
            isGrounded = true;
        }

        // platforms that provide direct calculation
        // if(obstacle.gameObject.CompareTag("Plat_Modify"))
        // {
        //     // 获取与Collider关联的GameObject上的TextMeshPro组件
        //     TextMeshPro platTmp = obstacle.transform.GetComponentInChildren<TextMeshPro>();

        //     if(platTmp != null)
        //     {
        //         string platText = platTmp.text;
        //         char operatorChar = platText[0]; // 获取字符串的第一个字符作为运算符
        //         int number;

        //         // 确保从第二个字符开始到结束的字符串是一个有效的数字
        //         if(int.TryParse(platText.Substring(1), out number))
        //         {
        //             // 根据运算符执行计算
        //             switch(operatorChar)
        //             {
        //                 case '+':
        //                     currentX += number;
        //                     break;
        //                 case '-':
        //                     currentX -= number;
        //                     break;
        //                 case '*':
        //                     currentX *= number;
        //                     break;
        //                 case '/':
        //                     if(number != 0) 
        //                         currentX /= number;
        //                     else 
        //                         Debug.LogError("Division by zero is not allowed.");
        //                     break;
        //                 default:
        //                     Debug.LogError("Invalid operator.");
        //                     break;
        //             }
        //             // 更新UI或其他相关组件
        //             xBoard.text = currentX.ToString();
        //             Debug.Log("Updated currentX: " + currentX);
        //         }
        //         else
        //         {
        //             Debug.LogError("Invalid number format on platform.");
        //         }
        //     }
        // }


    }

    public void UpdateScore(int increaseX)
    {
        GlobalVariables.collisions++; // Chris: data collection

        switch (operatorID)
        {
            case 0:
                if (negativeX(currentX, increaseX))
                    return;
                currentX += increaseX;
                break;
            case 1:
                if (negativeX(currentX, increaseX))
                    return;
                currentX -= increaseX;
                break;
            case 2:
                if (negativeX(currentX, increaseX))
                    return;
                currentX *= increaseX;
                break;
            case 3:
                if (negativeX(currentX, increaseX))
                    return;
                currentX /= increaseX;
                break;
            case 4:
                hintDisplay = "Select an Operator";
                ShowHint(hintDisplay);
                StartCoroutine(HideHint(1));
                break;
        }

        xBoard.text = currentX.ToString();
    }


    void OnCollisionExit2D(Collision2D obstacle)
    {
        // Jump Disabled
        if (obstacle.gameObject.CompareTag("Ground") || obstacle.gameObject.CompareTag("Platform_Solid") || obstacle.gameObject.CompareTag("Platform_Mutate") || obstacle.gameObject.CompareTag("Destination"))
            isGrounded = false;

        if (obstacle.gameObject.CompareTag("Portal"))
        {
            StartCoroutine(HideHint(0)); // Hide the hint immediately
        }
    }


    //teleporter
    void OnTriggerEnter2D(Collider2D obstacle)
    {
        //update score
        if (obstacle.gameObject.CompareTag("Number"))
        {
            currentCollidingObject = obstacle; // Store the collider for use in Update
        }
        if (obstacle.gameObject.CompareTag("Portal"))
        {
            TextMeshPro portalEquationText = obstacle.gameObject.GetComponentInChildren<TextMeshPro>();
            GameObject obj = GameObject.Find("EquationManager");
            JudgeEquation judge = obj.GetComponent<JudgeEquation>();
            TextMeshPro playerEquationText = GetComponentInChildren<TextMeshPro>();

            if (portalEquationText == null || portalEquationText.text.Length == 0 || playerEquationText != null)
            {
                bool equationSatisfied = true;

                if (portalEquationText != null && portalEquationText.text.Length > 0)
                {
                    int playerNumber;
                    if (int.TryParse(playerEquationText.text, out playerNumber))
                    {
                        equationSatisfied = judge.CheckEquation(portalEquationText.text, playerNumber);
                    }
                    else
                    {
                        equationSatisfied = false;
                    }
                }

                if (equationSatisfied)
                {
                    ShowHint("Press 'E' to teleport");
                }
                else
                {
                    ShowHint("Your point doesn't satisfy the condition");
                }

                if (equationSatisfied)
                {
                    string pairName = obstacle.gameObject.name.EndsWith("1") ?
                        obstacle.gameObject.name.Replace("1", "2") :
                        obstacle.gameObject.name.Replace("2", "1");
                    GameObject pairPortal = GameObject.Find(pairName);
                    if (pairPortal != null)
                    {
                        nearbyTeleporterDestination = pairPortal.transform;  // set paired portal
                    }
                }

            }
        }

    }


    void OnTriggerExit2D(Collider2D obstacle)
    {
        if (obstacle.gameObject.CompareTag("Portal"))
        {
            nearbyTeleporterDestination = null;
            StartCoroutine(HideHint(0));
        }
        currentCollidingObject = null;
    }

    void DisableLayerCollision(GameObject Platform)
    {
        Platform.layer = LayerMask.NameToLayer("Fake");
    }

    void EnableLayerCollision(GameObject Platform)
    {
        Platform.layer = LayerMask.NameToLayer("Platform");
    }


    bool CanPassPlatform(GameObject obstacle)
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

            // 获取玩家上的TextMeshProUGUI组件的值
            GameObject player = GameObject.Find("Player");
            judge.playerEquationText = player.GetComponentInChildren<TextMeshPro>();
            // Debug.Log(judge.playerEquationText.text);

            if (judge.playerEquationText != null)
            {
                judge.varValue = int.Parse(judge.playerEquationText.text);
            }
            // Debug.Log(judge.varValue);

            // 调用EvaluateFromTextMeshPro方法来检查玩家的数值是否满足方程
            pass = !judge.EvaluateFromTextMeshPro(); // if 
        }
        return pass;
    }


    private void ReturnToMainMenu()
    {
        // 加载主菜单场景，假设场景的名字为"MainMenu"
        SceneLoader.GetComponent<Transition>().LoadGameOverWon();
    }

    //test_ball

    void PickupNearestNumber()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Test_ball");

        if (balls.Length == 0)
        {
            // 如果没有球体对象，不执行任何操作
            return;
        }

        // 计算最近的球体对象
        float minDistance = float.MaxValue;
        foreach (GameObject ball in balls)
        {
            float distance = Vector3.Distance(transform.position, ball.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestBall = ball;
            }
        }

        // 捡起最近的球体（你可以在这里执行自定义操作，例如改变球体的父对象）
        if (nearestBall != null)
        {
            // 获取玩家头部位置
            Vector3 playerHeadPosition = transform.position + new Vector3(0, 2, 0); // 假设头部相对于玩家位置的偏移是 (0, 2, 0)

            // 设置球体位置为玩家头部位置
            nearestBall.transform.position = playerHeadPosition;
        }

    }

    // Set the player's boundary to the ground's boundary. 
    // Initialize player's left and right bound.
    private void SetPlayerBoundary()
    {
        // Find the GameObject with the "ground" tag.
        GameObject groundObject = GameObject.FindGameObjectWithTag("Ground");

        // Check if a GameObject with the "ground" tag was found.
        if (groundObject != null)
        {
            // Get the Collider2D component from the ground GameObject.
            groundCollider = groundObject.GetComponent<Collider2D>();

            // Check if the Collider2D component was found.
            if (groundCollider != null)
            {
                playerCollider = gameObject.GetComponent<Collider2D>();

                // Calculate the minimum and maximum X values based on the ground's bounds.
                Bounds groundBounds = groundCollider.bounds;
                Bounds playerBounds = playerCollider.bounds;

                leftBound = groundBounds.min.x + playerBounds.extents.x;
                rightBound = groundBounds.max.x - playerBounds.extents.x;

                // You now have access to the ground's Collider2D component.
                // You can use groundCollider in your code.
            }
            else
            {
                Debug.LogError("Ground GameObject doesn't have a Collider2D component.");
            }
        }
        else
        {
            Debug.LogError("No GameObject with the 'ground' tag found in the scene.");
        }
    }

    private void KeepPlayerInBound()
    {
        // Keep the player in bound
        // Get the current player position.
        Vector3 newPosition = transform.position;

        // Constrain the player's X position within the boundaries.
        newPosition.x = Mathf.Clamp(newPosition.x, leftBound, rightBound);

        // Update the player's position.
        transform.position = newPosition;
    }

}