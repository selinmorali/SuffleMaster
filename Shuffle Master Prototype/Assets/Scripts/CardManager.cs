using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject LeftHand, RightHand;

    public static CardManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    public void MoveCard(int count, HandSO.Side handSide)
    {
        RemoveCard(count, handSide);
        AddCard(count, handSide);
    }

    public void RemoveCard(int count, HandSO.Side handSide)
    {
        if(handSide == HandSO.Side.Left)
        {

        }
    }

    public void AddCard(int count, HandSO.Side handSide)
    {

    }
}
