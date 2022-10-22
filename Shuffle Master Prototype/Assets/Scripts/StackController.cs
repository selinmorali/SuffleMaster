using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackController : MonoBehaviour
{
    private float _cardHeight;
    private float _placeToCardHeightPosition;
    private GameObject _topDeckCard;
    private Vector3 _topDeckCardPosition;
    private Vector3 placeToCardPosition;
    private Stack<GameObject> currentStack;
    private bool isStarted = false;
    private int currentCardCount;
    private int lastCardCount;
    public enum Side
    {
        Left,
        Right
    }

    public Side side;

    private void Start()
    {
        currentStack = new Stack<GameObject>();
        _cardHeight = Card.Instance.CardHeight;
        StartCoroutine(nameof(GetPositionForNewCard));
        currentCardCount = currentStack.Count;
        lastCardCount = currentStack.Count;
    }
    private void Update()
    {
        if (!isStarted)
        {
            if (side == Side.Left)
            {
                GetCardAndPlace(10, Side.Left.ToString());
                isStarted = true;
            }
        }
    }
    private GameObject GetCardFromPool(string side)
    {
        GameObject card = ObjectPooler.Instance.GetCardFromPool(side);

        return card;
    }

    private void PlaceCardToDeck(GameObject card)
    {
        card.SetActive(true);
        card.transform.position = GetPositionForNewCard();
        currentStack.Push(card);
    }

    private void GetCardAndPlace(int count, string side)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temporaryCard = GetCardFromPool(side);
            PlaceCardToDeck(temporaryCard);
        }
    }

    private void RemoveCardFromDeck(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject cardToRemove = currentStack.Pop();
            cardToRemove.SetActive(false);
        }
    }

    private Vector3 GetPositionForNewCard()
    {
        if (currentStack.Count > 0)
        {
            _topDeckCard = currentStack.Peek();
            _topDeckCardPosition = _topDeckCard.transform.position;
            _placeToCardHeightPosition = _topDeckCardPosition.y + _cardHeight;
            placeToCardPosition = new Vector3(_topDeckCard.transform.position.x, _placeToCardHeightPosition, _topDeckCard.transform.position.z);

            return placeToCardPosition;
        }
        else
        {
            placeToCardPosition = gameObject.transform.GetChild(3).transform.position;

            return placeToCardPosition;
        }
    }
}
