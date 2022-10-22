using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HandSO", menuName = "ScriptableObjects/HandSO", order = 1)]
public class HandSO : ScriptableObject
{
    public enum Side
    {
        Left,
        Right
    }

    public Side side;

    public Stack<GameObject> CurrentHand = new Stack<GameObject>();
}
