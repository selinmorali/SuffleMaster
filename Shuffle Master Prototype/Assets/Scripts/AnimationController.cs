using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Vector3 _animFirstPosition;
    private GameObject _topRightCard;
    private GameObject _topLeftCard;
    private Vector3 _animLastPosition;
    public GameObject CardPrefab;


    private void Start()
    {
        //kart taþýma iþlemi varsa,
        if (gameObject.name == "LeftHand")
        {
            if (gameObject.GetComponent<StackController>().currentStack.Count > 0) //elde kart varsa
            {
                /*_topLeftCard = gameObject.GetComponent<StackController>().currentStack.Peek();*/ //soldaki en üstteki karta eriþtim.
                _animFirstPosition = _topLeftCard.transform.position; //en üstteki kartýn pozisyonunu aldým. Animasyon burada baþlayacak.
                //bu pozisyona kart ekleyeceðim.

                gameObject.GetComponent<StackController>().PlaceCardToDeck(CardPrefab);

                _topRightCard = gameObject.GetComponent<StackController>().currentStack.Peek();
                _animLastPosition = _topRightCard.transform.position;

            }

            //Soldan animasyon
        }
        if (gameObject.name == "RightHand")
        {
            if (gameObject.GetComponent<StackController>().currentStack.Count > 0) //elde kart varsa
            {

            }
            //Saðdan sola animasyon
        }
    }
}
