using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    private TMP_Text text;
    public float speed = 40f;
    public float scaleSpeed = 10f;
    private float elapsedTime;
    private RectTransform rectTransform;  // 添加RectTransform的引用
    private bool isTextChanging = false;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        rectTransform = GetComponent<RectTransform>();  // 获取RectTransform组件
        if (text != null && rectTransform != null)
        {
            rectTransform.anchoredPosition = Vector3.zero;
            elapsedTime = 0f;
        }
    }

    void Update()
    {
        if (text != null && rectTransform != null && !isTextChanging)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log("TEXT:" + text.text);
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
                text.text = "";
            }
        }
    }

    public void SetTextValues(string operatorID, string increaseX)
    {
        isTextChanging = true;
        // 在这里更新文本的显示内容
        Debug.Log(operatorID);
        text.text = operatorID + " " + increaseX;
        rectTransform.anchoredPosition = Vector3.zero + new Vector3(0, 5, 0);
        text.fontSize = 45;
        elapsedTime = 0f;
        isTextChanging = false;
    }
}
