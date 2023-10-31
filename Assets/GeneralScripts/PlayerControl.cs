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
    public int currentX;
    public GameObject xObject;
    public TMP_Text xBoard;
    public GameObject WheelManager;
    public int operatorID;
    public float xBound;
    public int gravityDirection;
    public TMP_Text hintText;
    private Rigidbody2D playerRB;
    private Vector2 force;
    private int increaseX;
    private bool isGrounded;
    private Vector2 invertedGravity;
    private int previousResult;
    private string hintDisplay;
    private JudgeEquation judgeEquation;
    private LayerMask playerLayer;
    private LayerMask platformLayer;
    private float effectDuration = 0.8f;
    private Transform nearbyTeleporterDestination;
    private List<GameObject> platforms;
    // private float startTime;

    private int cnt = 0;

    void Start()
    {
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
        operatorID = 0;
        hintText.gameObject.SetActive(false);

        playerLayer = LayerMask.NameToLayer("Player");
        platformLayer = LayerMask.NameToLayer("Platform");
        platforms = new List<GameObject>();
        // These are the platforms that change dynamically
        platforms.AddRange(GameObject.FindGameObjectsWithTag("Platform"));
        platforms.AddRange(GameObject.FindGameObjectsWithTag("Fake"));
        resolvePlatforms();
    }

    void Update()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
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
            if (gravityDirection == 1 ||(SceneManager.GetActiveScene().name == "Level1") || (SceneManager.GetActiveScene().name == "Level2")|| (SceneManager.GetActiveScene().name == "Tutorial_Portal_Geng")
                || (SceneManager.GetActiveScene().name == "Level1_Portal_Geng 1") || (SceneManager.GetActiveScene().name == "Level1_Monster_Chris") || (SceneManager.GetActiveScene().name == "Level2_Monster_Chris"))
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
        if (nearbyTeleporterDestination != null && Input.GetKeyDown(KeyCode.E))
        {
            transform.position = nearbyTeleporterDestination.position; 
        }

        // Flip Gravity
        if ((SceneManager.GetActiveScene().name != "Level1") && (SceneManager.GetActiveScene().name != "Level2") && (SceneManager.GetActiveScene().name != "Tutorial_Portal_Geng")
            && (SceneManager.GetActiveScene().name != "Level1_Portal_Geng 1") && (SceneManager.GetActiveScene().name != "Level1_Monster_Chris") && (SceneManager.GetActiveScene().name != "Level2_Monster_Chris"))
        {
            if (currentX >= 0)
                Physics2D.gravity = invertedGravity;
            else
                Physics2D.gravity = (float)-0.5 * invertedGravity;
        }
    }

    // Sets the platform logic at start and whenever currentX changes
    void resolvePlatforms()
    {
        // Check all platforms and separate solid platforms
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
                result /= increment;
                hintDisplay = "Dividing " + increment;
                break;
        }
        string sceneName = SceneManager.GetActiveScene().name;
        if ((SceneManager.GetActiveScene().name != "Level1") && (SceneManager.GetActiveScene().name != "Level2") && (SceneManager.GetActiveScene().name != "Tutorial_Portal_Geng")
            && (SceneManager.GetActiveScene().name != "Level1_Portal_Geng 1") && (SceneManager.GetActiveScene().name != "Level1_Monster_Chris") && (SceneManager.GetActiveScene().name != "Level2_Monster_Chris")){
            if(result < 0)
            {
                // Flip Gravity Logic;
                if(previousResult >= 0)
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




    void OnCollisionEnter2D(Collision2D obstacle)
    {
        // 如果玩家与一个障碍物碰撞
        if (obstacle.gameObject.CompareTag("Ground") || obstacle.gameObject.CompareTag("Destination"))
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
            resolvePlatforms();
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
        }

    }


    void OnCollisionExit2D(Collision2D obstacle)
    {
        // Jump Disabled
        if (obstacle.gameObject.CompareTag("Ground") || obstacle.gameObject.CompareTag("Platform") || obstacle.gameObject.CompareTag("Destination"))
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
