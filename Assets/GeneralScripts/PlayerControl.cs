using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour
{
    public float horizontalInput;
    public float speed;
    public float jumpForce;
    public float acceleration;
    public float groundDeceleration;
    public float airDeceleration;
    public float initialVelocity;
    public int currentX;
    public GameObject xObject;
    public TMP_Text xBoard;
    public GameObject WheelManager;
    public int operatorID;
    public float xBound;
    public TMP_Text hintText;
    public int velocityLimit;
    private Vector2 accelerationVector;
    private Rigidbody2D playerRB;
    private Vector2 force;
    private int increaseX;
    private bool isGrounded;
    private JudgeEquation judgeEquation;
    private LayerMask playerLayer;
    private LayerMask platformLayer;
    private float effectDuration = 0.8f;
    private Transform nearbyTeleporterDestination;
    // private float startTime;

    private int cnt = 0;

    void Start()
    {
        // 查找所有包含 "platform" 字符串的游戏对象
        GameObject[] platformObjects = FindObjectsWithSubstring("Platform");

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
        operatorID = 0;
        hintText.gameObject.SetActive(false);

        playerLayer = LayerMask.NameToLayer("Player");
        platformLayer = LayerMask.NameToLayer("Platform");

    }

    void Update()
    {
        // Move back and forth
        horizontalInput = Input.GetAxis("Horizontal");
        
        if (!isGrounded)
        {
            // Gliding motion in Air
            transform.Translate(Vector2.right * Time.deltaTime * (speed) * horizontalInput);
        }
       
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
        if (WheelManager != null)
        {
            operatorID = WheelManager.GetComponent<WheelController>().operatorID;
        }
        
        //press 'E' to teleport
        if (nearbyTeleporterDestination != null && Input.GetKeyDown(KeyCode.E))
        {
            transform.position = nearbyTeleporterDestination.position; 
        }
        
    }

    void FixedUpdate()
    {
         if(isGrounded)
        {
            // Accelerated Run Motion
            if (playerRB.velocity.magnitude < velocityLimit && horizontalInput != 0)
            {
                accelerationVector = Vector2.right * acceleration;
                // Setting initial velocity
                if (playerRB.velocity.magnitude == 0)
                {
                    if (horizontalInput > 0)
                        playerRB.velocity = Vector2.right * initialVelocity;

                    if (horizontalInput < 0)
                        playerRB.velocity = -1 * Vector2.right * initialVelocity;
                }
                
                // Setting constant acceleration
                if(horizontalInput > 0)
                    playerRB.velocity += accelerationVector * Time.fixedDeltaTime;

                if (horizontalInput < 0)
                    playerRB.velocity -= accelerationVector * Time.fixedDeltaTime;
                // Make sure the velocity doesn't exceed the maximum
                playerRB.velocity = Vector2.ClampMagnitude(playerRB.velocity, velocityLimit);
            }
            // Decelerate constantly ground friction
            if(playerRB.velocity.magnitude > 0 && horizontalInput == 0)
            {
                Vector2 decelerationVector = -playerRB.velocity.normalized * groundDeceleration;
                playerRB.velocity += decelerationVector * Time.fixedDeltaTime;
            }
        }
        else
        {
            // Decelerate constantly air friction
            if (playerRB.velocity.magnitude > 0 && horizontalInput == 0)
            {
                Vector2 decelerationVector = -playerRB.velocity.normalized * airDeceleration;
                playerRB.velocity += decelerationVector * Time.fixedDeltaTime;
            }
        }
        Debug.Log(playerRB.velocity.magnitude);
    }

    void ShowHint(string hint)
    {
        hintText.text = hint;
        hintText.gameObject.SetActive(true);
    }

    private IEnumerator HideHint(int delay)
    {
        yield return new WaitForSeconds(delay);
        hintText.gameObject.SetActive(false);
    }

    bool negativeX(int result, int increment)
    {
        switch (operatorID)
        {
            case 0:
                result += increment;
                if (result >= 0)
                    ShowHint("Adding " + increment);
                break;
            case 1:
                result -= increment;
                if (result >= 0)
                    ShowHint("Subracting " + increment);
                break;
            case 2:
                if (result >= 0)
                    ShowHint("Multiplying " + increment);
                result *= increment;
                break;
            case 3:
                if (result >= 0)
                    ShowHint("Dividing " + increment);
                result /= increment;
                break;
        }
        if (result >= 0)
        {
            StartCoroutine(HideHint(1));
            return false;
        }

        else
        {
            ShowHint("Cannot be a Negative number!");
            StartCoroutine(HideHint(3));
            return true;
        }
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




    void OnCollisionEnter2D(Collision2D obstacle)
    {
        // 如果玩家与一个障碍物碰撞
        if (obstacle.gameObject.CompareTag("Ground"))
        {
            // Jump Enabled
            isGrounded = true;
        }

        // Score update
        if (obstacle.gameObject.CompareTag("Number"))
        {
            GlobalVariables.collisions++;
            increaseX = int.Parse(Regex.Match(obstacle.gameObject.name, @"\d+$").Value);
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
            }
            Destroy(obstacle.gameObject);
            xBoard.text = currentX.ToString();
        }

        if (obstacle.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Goal");
            GlobalVariables.win = true;
            ReturnToMainMenu();
        }
        // if Player collides with a platform
        if (obstacle.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            bool canPass = CanPassPlatform(obstacle);
            // Debug.Log(canPass);
            if (canPass)
            {
                StartCoroutine(ActivateEffect(obstacle.gameObject));
            }
        }

    }


    void OnCollisionExit2D(Collision2D obstacle)
    {
        // Jump Disabled
        if (obstacle.gameObject.CompareTag("Ground") || obstacle.gameObject.CompareTag("Platform"))
            isGrounded = false;

    }

    //teleporter
    void OnTriggerEnter2D(Collider2D obstacle)
    {
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
    }

    private IEnumerator ActivateEffect(GameObject Platform)
    {
        // Code for the effect to start here
        DisableLayerCollision(Platform);

        yield return new WaitForSeconds(effectDuration);

        // Code for the effect to end here
        EnableLayerCollision(Platform);
    }

    void DisableLayerCollision(GameObject Platform)
    {
        Platform.layer = LayerMask.NameToLayer("Fake");
    }

    void EnableLayerCollision(GameObject Platform)
    {
        Platform.layer = LayerMask.NameToLayer("Platform");
    }


    bool CanPassPlatform(Collision2D obstacle)
    {
        bool pass = false;

        // Platform Equation Logic
        TextMeshPro obstacleEquationText = obstacle.gameObject.GetComponentInChildren<TextMeshPro>();
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

            // 调用EvaluateFromTextMeshPro方法来检查玩家的数值是否满足方程
            pass = !judge.EvaluateFromTextMeshPro(); // if 
        }
        return pass;
    }


    private void ReturnToMainMenu()
    {
        // 加载主菜单场景，假设场景的名字为"MainMenu"
        SceneManager.LoadScene("Game Over");
    }
}
