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
        //kart ta��ma i�lemi varsa,
        if (gameObject.name == "LeftHand")
        {
            if (gameObject.GetComponent<StackController>().currentStack.Count > 0) //elde kart varsa
            {
                /*_topLeftCard = gameObject.GetComponent<StackController>().currentStack.Peek();*/ //soldaki en �stteki karta eri�tim.
                _animFirstPosition = _topLeftCard.transform.position; //en �stteki kart�n pozisyonunu ald�m. Animasyon burada ba�layacak.
                //bu pozisyona kart ekleyece�im.

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
            //Sa�dan sola animasyon
        }
    }
}
