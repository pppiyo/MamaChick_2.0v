using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float speed = 40f;
    public float scaleSpeed = 10f;
    private float elapsedTime;
    private RectTransform rectTransform;  // 添加RectTransform的引用
    private bool isTextChanging = false;

    public Transform player; // 指向Player对象的Transform组件
    public Vector3 offset = new Vector3(0, 50, 0); // 文本在Player头顶的偏移量



    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();  // 获取RectTransform组件
        if (text != null && rectTransform != null)
        {
            elapsedTime = 0f;
        }
        
    }

    void Update()
    {
        if (text != null && rectTransform != null && !isTextChanging)
        {
            elapsedTime += Time.deltaTime;

            // Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position + offset);
            // text.rectTransform.position = screenPos + Vector3.up * speed * Time.deltaTime;


            // 在垂直方向上自动向上移动
            text.rectTransform.position += Vector3.up * speed * Time.deltaTime;

            // 打印物体的当前 anchoredPosition
            // Debug.Log("Current Anchored Position: " + rectTransform.anchoredPosition);

            // 颜色和字体大小的变化
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Clamp01(1f - elapsedTime / 2f));
            text.fontSize += scaleSpeed * Time.deltaTime;

            // 判断是否达到销毁条件
            if (elapsedTime >= 2f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetTextValues(string operatorID, string increaseX)
    {
        isTextChanging = true;
        // 在这里更新文本的显示内容
        text.text = operatorID + " " + increaseX;
        isTextChanging = false;
    }
}
