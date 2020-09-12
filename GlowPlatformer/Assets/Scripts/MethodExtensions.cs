using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MethodExtensions
{
    public static string CleanSocketData(this string Value)
    {
        Value = Value.Replace("}", " ");
        Value = Value.Replace("\"", " ");
        Value = Value.Remove(0, 7);
        return Value;
    }
}