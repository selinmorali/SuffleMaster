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
    public Stack<GameObject> currentStack;
    private bool isStarted = false;
    private int currentCardCount;
    private int lastCardCount;
    [SerializeField] private HandSO handSO;
    [SerializeField]

    private void Start()
    {
        currentStack = handSO.CurrentHand;
        _cardHeight = Card.Instance.CardHeight;
        StartCoroutine(nameof(GetPositionForNewCard));
        currentCardCount = currentStack.Count;
        lastCardCount = currentStack.Count;
    }

    private void Update()
    {
        if (!isStarted)
        {
            if (handSO.side == HandSO.Side.Left)
            {
                GetCardAndPlace(50);
                isStarted = true;
            }
        }
    }

    private GameObject GetCardFromPool(string handSide)
    {
        GameObject card = ObjectPooler.Instance.GetCardFromPool(handSide);

        return card;
    }

    public void PlaceCardToDeck(GameObject card)
    {
        card.SetActive(true);
        card.transform.position = GetPositionForNewCard();
        currentStack.Push(card);
    }

    public void GetCardAndPlace(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temporaryCard = GetCardFromPool(handSO.side.ToString());
            PlaceCardToDeck(temporaryCard);
        }
    }

    public void RemoveCardFromDeck(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (currentStack.Count > 0)
            {
                GameObject cardToRemove = currentStack.Pop();
                cardToRemove.SetActive(false);
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate") && currentStack.Count > 0)
        {
            if (other.GetComponent<Card>()._operator == "+")
            {
                GetCardAndPlace(other.GetComponent<Card>().value);
            }
            else if (other.GetComponent<Card>()._operator == "-")
            {
                RemoveCardFromDeck(other.GetComponent<Card>().value);
            }
            else if (other.GetComponent<Card>()._operator == "*")
            {
                int result = currentStack.Count * (other.GetComponent<Card>().value - 1);

                GetCardAndPlace(result);
            }
        }
    }
}
