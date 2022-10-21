using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    private float _cardHeight;
    private float _placeToCardHeightPosition;
    private GameObject _topDeckCard;
    private Vector3 _topDeckCardPosition;
    private Vector3 placeToCardPosition;
    public Stack<GameObject> currentStack;
    public HandSO handSO;
    private bool isStarted = false;

    private void Start()
    {
        _cardHeight = Card.Instance.CardHeight;
        currentStack = handSO.CurrentStack;
        StartCoroutine(nameof(CheckCardsOnStack));
    }
    private void Update()
    {
        if (!isStarted)
        {
            if (handSO.side == HandSO.Side.Left)
            {
                StackTenCards();
                isStarted = true;
            }
        }
    }

    private void StackTenCards()
    {

        for (int i = 0; i < 10; i++)
        {
            ObjectPooler.Instance.GetCardToHandFromPool("leftObjectPool", placeToCardPosition, currentStack);
        }
    }

    IEnumerator CheckCardsOnStack()
    {
        while (true)
        {
            if (currentStack.Count != 0)
            {
                _topDeckCard = currentStack.Peek();
                _topDeckCardPosition = _topDeckCard.transform.position;
                _placeToCardHeightPosition = _topDeckCardPosition.y + _cardHeight;
                placeToCardPosition = new Vector3(_topDeckCard.transform.position.x, _placeToCardHeightPosition, _topDeckCard.transform.position.z);

            }
            else
            {
                placeToCardPosition = gameObject.transform.GetChild(3).transform.position;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
