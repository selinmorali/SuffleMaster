using UnityEngine;

public class StackManager : MonoSingleton<StackManager>
{
    private float _cardHeight = 0.016f;
    private float _placeToCardPositionY;
    private Vector3 _placeToCardPosition;

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
        card.transform.localPosition = GetLocalPositionForNewCard(hand);
        hand.currentStack.Push(card);
    }

    //Kartin eklenecegi local pozisyon alma islemi
    public Vector3 GetLocalPositionForNewCard(Hand hand)
    {
        if (hand.currentStack.Count > 0)
        {
            _placeToCardPositionY = hand.firstCardPosition.y + (_cardHeight * hand.currentStack.Count);
            _placeToCardPosition = new Vector3(hand.firstCardPosition.x, _placeToCardPositionY, hand.firstCardPosition.z);
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
                cardToRemove.SetActive(false);
            }
        }
    }
}
