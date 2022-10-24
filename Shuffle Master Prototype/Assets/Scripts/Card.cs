using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public static Card Instance;
    public float CardHeight;


    void Awake()
    {
        Instance = this;

        CardHeight = gameObject.GetComponent<MeshRenderer>().bounds.size.y;
    }
}
