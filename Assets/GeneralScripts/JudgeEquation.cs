using System;
using System.Text.RegularExpressions;
using TMPro; // 包含TextMeshPro命名空间
using UnityEngine;
using org.mariuszgromada.math.mxparser; // 新增：mXparser的命名空间

public class JudgeEquation : MonoBehaviour
{
    public TextMeshPro playerEquationText;
    public TextMeshPro equationText;
    public int varValue;
    public GameObject targetObject;

    void Update()
    {
        if (GlobalVariables.curLevel == 1)
        {
            if(curX > 30)
            {
                // EndGame
            }
        }

        else if(GlobalVariables.curLevel == 2)
        {
            if (curX > 30)
            {
                // EndGame
            }
        }
    }
    private double Evaluate(string expression)
    {
        expression = expression.Replace("x", varValue.ToString());
        Expression e = new Expression(expression);
        return e.calculate();
    }

    public bool CheckEquation(string equationStr, int varValue)
    {
        if (equationStr.Trim().ToLower() == "even")
        {
            return varValue % 2 == 0;
        }
        if (equationStr.Trim().ToLower() == "odd")
        {
            return varValue % 2 != 0;
        }

        var match = Regex.Match(equationStr, @"(.+)(<=|>=|<|>|!=|==)([^=]+)");
        if (!match.Success)
            return false;

        string leftExpr = match.Groups[1].Value.Trim();
        string operatorStr = match.Groups[2].Value.Trim();
        string rightExpr = match.Groups[3].Value.Trim();
        
        double leftValue = Evaluate(leftExpr);
        double rightValue = Evaluate(rightExpr);

        Debug.Log("left:"+ leftValue);
        Debug.Log("right:"+ rightValue);

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

    public bool EvaluateFromTextMeshPro()
    {
        string equationStr = equationText.text;
        bool result = CheckEquation(equationStr, varValue);
        return result;
    }
}