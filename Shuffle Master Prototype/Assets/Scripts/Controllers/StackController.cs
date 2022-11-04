using UnityEngine;

public class StackController : MonoSingleton<StackController>
{
    private GameObject _topDeckCard;
    private float _placeToCardPositionY;
    private Vector3 _topDeckCardPosition;
    private Vector3 _placeToCardPosition;
    [SerializeField] private GameObject _queue;
    [SerializeField] private GameObject _mainPool;

    private void Start()
    {
        //Oyun baslayinca sol elime 10 kart aliyorum
        DrawTenCards();
    }

    //Oyun baslayinca sol ele 10 kart koyma islemi
    public void DrawTenCards()
    {
        GetCardAndPlace(LeftHand.Instance, count: 10);
    }

    //Object pooldan kart cekip yerlestirme islemi
    public void GetCardAndPlace(Hand hand, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temporaryCard = ObjectPooler.Instance.GetCard();
            PlaceCardToDeck(hand, temporaryCard);
        }
    }

    //Karti desteye ekleme islemi
    public void PlaceCardToDeck(Hand hand, GameObject card)
    {
        card.SetActive(true);
        card.transform.parent = _queue.transform;
        card.transform.localPosition = GetLocalPositionForNewCard(hand);
        hand.currentStack.Push(card);
    }

    //Kartin eklenecegi local pozisyon alma islemi
    public Vector3 GetLocalPositionForNewCard(Hand hand)
    {
        if (hand.currentStack.Count > 0)
        {
            _topDeckCard = hand.currentStack.Peek();
            _topDeckCardPosition = _topDeckCard.transform.localPosition;
            _placeToCardPositionY = _topDeckCardPosition.y + 0.016f;
            _placeToCardPosition = new Vector3(_topDeckCard.transform.localPosition.x, _placeToCardPositionY, _topDeckCard.transform.localPosition.z);

            return _placeToCardPosition;
        }
        else
        {
            _placeToCardPosition = hand.firstCardPosition;
            return _placeToCardPosition;
        }
    }

    //Eldeki desteden kart cikarma islemi
    public void RemoveCardFromDeck(Hand hand, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (hand.currentStack.Count > 0)
            {
                GameObject cardToRemove = hand.currentStack.Pop();
                cardToRemove.transform.parent = _mainPool.transform;
                cardToRemove.SetActive(false);
            }
        }
    }
}
