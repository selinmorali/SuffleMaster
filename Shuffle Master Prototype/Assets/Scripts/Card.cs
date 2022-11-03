using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public static Card Instance;

    void Awake()
    {
        Instance = this;
    }

    public float GetCardHeight()
    {
        //Kartin yukseklik degerini alma islemi
        float _cardHeight = gameObject.GetComponent<MeshRenderer>().bounds.size.y;
        return _cardHeight;
    }
}
