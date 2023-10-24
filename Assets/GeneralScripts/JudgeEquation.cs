using System;
using System.Text.RegularExpressions;
using TMPro; // 包含TextMeshPro命名空间
using UnityEngine;

public class JudgeEquation : MonoBehaviour
{
    // 在Inspector中为这两个变量拖放TextMeshPro组件
    public TextMeshPro playerEquationText; // 新增：玩家上的TextMeshProUGUI组件
    public TextMeshPro equationText; // 障碍物上的TextMeshProUGUI组件
    public int varValue;
    public GameObject targetObject;
    private double Evaluate(string expression)
    {
        var termTokens = Regex.Split(expression, @"([-+])");
        double value = ParseFactor(termTokens[0]);

        for (int i = 1; i < termTokens.Length; i += 2)
        {
            var op = termTokens[i];
            var termValue = ParseFactor(termTokens[i + 1]);

            switch (op)
            {
                case "+":
                    value += termValue;
                    break;
                case "-":
                    value -= termValue;
                    break;
            }
        }

        return value;
    }

    private double ParseFactor(string expression)
    {
        var factorTokens = Regex.Split(expression, @"([*/])");
        double value = ParseNumber(factorTokens[0]);

        for (int i = 1; i < factorTokens.Length; i += 2)
        {
            var op = factorTokens[i];
            var factorValue = ParseNumber(factorTokens[i + 1]);

            switch (op)
            {
                case "*":
                    value *= factorValue;
                    break;
                case "/":
                    value /= factorValue;
                    break;
            }
        }

        return value;
    }

    private double ParseNumber(string expression)
    {
        expression = expression.Trim();
        if (expression.StartsWith("("))
        {
            var closingBracketPos = expression.LastIndexOf(")");
            var innerExpression = expression.Substring(1, closingBracketPos - 1);
            return Evaluate(innerExpression);
        }

        // 改进处理负数
        expression = expression.Replace("--", "+");
        return double.Parse(expression);
    }

    public bool CheckEquation(string equationStr, int varValue)
    {
        // 检查是否为 "even" 或 "odd"
        if (equationStr.Trim().ToLower() == "even")
        {
            return varValue % 2 == 0; // 如果余数为0，那么是偶数
        }
        if (equationStr.Trim().ToLower() == "odd")
        {
            return varValue % 2 != 0; // 如果余数不为0，那么是奇数
        }

        // 如果不是 "even" 或 "odd"，继续原来的判断逻辑
        var match = Regex.Match(equationStr, @"(.+)(<=|>=|<|>|!=|==)([^=]+)");
        if (!match.Success)
            return false;

        string leftExpr = match.Groups[1].Value.Trim().Replace("x", varValue.ToString());
        string operatorStr = match.Groups[2].Value.Trim();
        string rightExpr = match.Groups[3].Value.Trim().Replace("x", varValue.ToString());

        double leftValue = Evaluate(leftExpr);
        double rightValue = Evaluate(rightExpr);

        switch (operatorStr)
        {
            case "<=": return leftValue <= rightValue;
            case ">=": return leftValue >= rightValue;
            case "<": return leftValue < rightValue;
            case ">": return leftValue > rightValue;
            case "!=": return leftValue != rightValue;
            case "==": return leftValue == rightValue;
            default: return false;
        }
    }


    public void EvaluateFromTextMeshPro()
    {
        string equationStr = equationText.text; 
        bool result = CheckEquation(equationStr, varValue);
        
        // 根据CheckEquation的结果为targetObject设置标签
        if (result)
        {
            targetObject.tag = "Ground";
            targetObject.layer = 0;
        }
        else
        {
            targetObject.tag = "Fake";
            targetObject.layer = 6;
        }
        
        Debug.Log(result);
    }
}
