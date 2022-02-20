using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomTool
{
    public static List<int> MakeRange(int num1, int num2)
    {
        int min = num1 < num2 ? num1 : num2;
        int max = num1 > num2 ? num1 : num2;

        List<int> result = new List<int>();

        result.Add(min);
        result.Add(max);

        return result;
    }

    // num1, num2를 extend만큼 확장했을 떄의 최솟값과 최댓값을 반환한다. 각각은 limit에 걸릴 경우 limit을 반환한다.
    public static List<int> MakeRange(int num1, int num2, int extend, int minLimit, int maxLimit)
    {
        int min = num1 < num2 ? num1 : num2;
        int max = num1 > num2 ? num1 : num2;

        min -= extend;
        max += extend;

        if (min < minLimit)
            min = minLimit;
        if (max > maxLimit)
            max = maxLimit;

        List<int> result = new List<int>();

        result.Add(min);
        result.Add(max);

        return result;
    }
    public static List<int> MakeRange(List<int> numList, int extend, int minLimit, int maxLimit)
    {
        int min = maxLimit;
        int max = minLimit;

        foreach (int num in numList)
        {
            if (num < min)
                min = num;
            if (num > max)
                max = num;
        }

        min -= extend;
        max += extend;

        if (min < minLimit)
            min = minLimit;
        if (max > maxLimit)
            max = maxLimit;

        List<int> result = new List<int>();

        result.Add(min);
        result.Add(max);

        return result;
    }

    public static Vector2 Vec2RadRotate(Vector2 vec, float delta)
    {
        return new Vector2(
            vec.x * Mathf.Cos(delta) - vec.y * Mathf.Sin(delta),
            vec.x * Mathf.Sin(delta) + vec.y * Mathf.Cos(delta)
        );
    }
    public static Vector2 Vec2RadRotate(float vx, float vy, float delta)
    {
        return new Vector2(
            vx * Mathf.Cos(delta) - vy * Mathf.Sin(delta),
            vx * Mathf.Sin(delta) + vy * Mathf.Cos(delta)
        );
    }
    public static Vector2 Vec2DegRotate(Vector2 vec, float delta)
    {
        return Vec2RadRotate(vec, Mathf.Deg2Rad * delta);
    }
    public static Vector2 Vec2DegRotate(float vx, float vy, float delta)
    {
        return Vec2RadRotate(vx, vy, Mathf.Deg2Rad * delta);
    }

    // y = mx + b의 형태인 방정식을 만든다. m, b가 차례로 들어간 list를 반환한다.
    // 만약 x = a 형태의 식이라면 NaN, a가 차례로 들어간 list를 반환한다.
    // y = a와 같은 형태의 식이라면 a만 들어간 list를 반환한다.
    public static List<float> MakeLineEquation(float x1, float y1, float x2, float y2)
    {
        List<float> equation = new List<float>();

        float dx = x2 - x1;
        float dy = y2 - y1;

        if (dx == 0)
        {
            equation.Add(float.NaN);
            equation.Add(x1);
            return equation;
        }
        if (dy == 0)
        {
            equation.Add(y1);
            return equation;
        }

        float m = dy / dx;
        float b = y1 - (m * x1);
        equation.Add(m);
        equation.Add(b);

        return equation;
    }
    public static List<float> MakeLineEquation(Vector2 v1, Vector2 v2)
    {
        return MakeLineEquation(v1.x, v1.y, v2.x, v2.y);
    }

    public static float GetYByLineEquation(List<float> equation, float x)
    {
        if (equation.Count == 1)
            return equation[0];

        if (equation[0] == float.NaN)
            return float.NaN;
        else
            return equation[0] * x + equation[1];
    }
    public static float GetYByLineEquation(float x1, float x2, float y1, float y2, float v)
    {
        float dx = x2 - x1;

        if (dx == 0)
            return float.NaN;

        float m = (y2 - y1) / dx;
        float b = y1 - (m * x1);

        return m * v + b;
    }

    public static float GetXByLineEquation(List<float> equation, float y)
    {
        if (equation.Count == 1)
            return equation[0];

        if (equation[0] == float.NaN)
            return float.NaN;
        else
            return (y - equation[1]) / equation[0];
    }
    public static float GetXByLineEquation(float x1, float x2, float y1, float y2, float v)
    {
        float dx = x2 - x1;

        if (dx == 0)
            return float.NaN;

        float m = (y2 - y1) / dx;
        float b = y1 - (m * x1);

        return (v - b) / m;
    }
}
