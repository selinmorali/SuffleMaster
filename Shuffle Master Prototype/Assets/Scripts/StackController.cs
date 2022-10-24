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
    [SerializeField] private HandSO handSO;
    [SerializeField] private GameObject _queue;
    [SerializeField] private GameObject mainPool;

    private void Start()
    {
        currentStack = handSO.CurrentHand;
        _cardHeight = Card.Instance.CardHeight;
    }

    private void Update()
    {
        if (!isStarted)
        {
            if (handSO.side == HandSO.Side.Left)
            {
                GetCardAndPlace(10);
                isStarted = true;
            }
        }
    }

    private GameObject GetCardFromPool()
    {
        GameObject card = ObjectPooler.Instance.GetCard();

        return card;
    }

    public void PlaceCardToDeck(GameObject card)
    {
        card.SetActive(true);
        card.transform.parent = _queue.transform;
        card.transform.position = GetPositionForNewCard();
        currentStack.Push(card);
    }

    public void GetCardAndPlace(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temporaryCard = GetCardFromPool();
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
                cardToRemove.transform.parent = mainPool.transform;
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
            if (other.GetComponent<Gate>()._operator == "+")
            {
                GetCardAndPlace(other.GetComponent<Gate>().value);
            }
            else if (other.GetComponent<Gate>()._operator == "-")
            {
                RemoveCardFromDeck(other.GetComponent<Gate> ().value);
            }
            else if (other.GetComponent<Gate>()._operator == "*")
            {
                int result = currentStack.Count * (other.GetComponent<Gate>().value - 1);

                GetCardAndPlace(result);
            }
        }
    }
}
