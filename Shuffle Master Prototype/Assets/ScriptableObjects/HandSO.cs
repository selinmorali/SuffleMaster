using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HandSO", order = 1)]
public class HandSO : ScriptableObject
{
    public enum Side
    {
        Left,
        Right
    }

    public Side side;
    public Stack<GameObject> CurrentStack = new Stack<GameObject>();
}
