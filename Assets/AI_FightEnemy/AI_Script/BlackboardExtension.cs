
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class BlackboardExtension
{
    public static void SetValueToDictionary<T>(this Dictionary<string, object> blackboard, string key, T value)
    {
        if(!blackboard.ContainsKey(key))
        {
            blackboard.Add(key, value);
            return;
        }

        blackboard[key] = value;
    }

    public static bool TryGetValueFromDictionary<T>(this Dictionary<string, object> blackboard, string key, out T result)
    {
        bool tempResult = blackboard.TryGetValue(key, out object temp);
        result = (T)temp;
        return tempResult;
    }
}