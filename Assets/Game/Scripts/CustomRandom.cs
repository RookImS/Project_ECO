using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CustomRandom
{
    public static T GetElement<T>(List<T> list)
    {
        int idx = Random.Range(0, list.Count);

        return list[idx];
    }

    public static List<T> GetElements<T>(int count, List<T> list)
    {
        List<T> result = new List<T>();
        List<T> candiate = new List<T>(list);

        int idx;
        for (int i = 0; i < count; ++i)
        {
            idx = Random.Range(0, candiate.Count);

            result.Add(candiate[idx]);
            candiate.RemoveAt(idx);
        }

        return result;
    }

    public static List<int> DistributeNumber(int distNum, int distRandListCount, List<int> limit)
    {
        if (distNum > limit.Sum())
        {
            Debug.LogError("distNum을 분배했을 때, list 내에 반드시 limit을 초과하는 원소가 생깁니다.");
            return null;
        }

        List<int> candiate = Enumerable.Range(0, distRandListCount).ToList();
        List<int> result = Enumerable.Repeat(0, distRandListCount).ToList();

        int idx;

        for(int i = 0; i < distNum; ++i)
        {
            idx = Random.Range(0, candiate.Count);
            ++result[candiate[idx]];

            if (result[candiate[idx]] + 1 > limit[candiate[idx]])
                candiate.RemoveAt(idx);
        }

        return result;
    }

    public static bool PickByProba(int proba)
    {
        if (proba < 0)
            proba = 0;
        else if (proba > 100)
            proba = 100;

        int randVal = Random.Range(0, 100);

        if (randVal < proba)
            return true;
        else
            return false;
    }

    public static int PickByProba(List<int> probaList)
    {
        int randVal = Random.Range(0, probaList.Sum());
        int cumulatedVal = 0;
        int resultIdx = 0;

        for (int i = 0; i < probaList.Count; ++i)
        {
            cumulatedVal += probaList[i];

            if (randVal < probaList[i])
                resultIdx = i;
        }

        return resultIdx;
    }
}
