using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static T ReturnRandomElement<T>(this List<T> myList)
    {
        return myList[Random.Range(0, myList.Count)];
    }
}
