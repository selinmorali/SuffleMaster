using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hand : MonoBehaviour
{
    public Stack<GameObject> currentStack;
    public Vector3 firstCardPosition;

    public enum Side
    {
        Left,
        Right
    }

    public Side side;
}