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
        //Oyun baslamadan once elime kart alýyorum
        if (!GameManager.Instance.isStarted)
        {
            if (handSO.side == HandSO.Side.Right)
            {
                GetCardAndPlace(10);
                GameManager.Instance.isStarted = true;
            }
        }
    }

    //Karti desteye ekleme islemi
    public void PlaceCardToDeck(GameObject card)
    {
        card.SetActive(true);
        card.transform.parent = _queue.transform;
        card.transform.position = GetPositionForNewCard();
        currentStack.Push(card);
    }

    //Object pooldan kart cekip yerlestirme islemi
    public void GetCardAndPlace(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temporaryCard = ObjectPooler.Instance.GetCard();
            PlaceCardToDeck(temporaryCard);
        }
    }

    //Eldeki desteden kart cikarma islemi
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

    //Eklenecek yeni kart icin pozisyon alma islemi
    public Vector3 GetPositionForNewCard()
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
            if(handSO.side == HandSO.Side.Left)
            {
                placeToCardPosition = gameObject.transform.parent.transform.GetChild(2).transform.GetChild(0).transform.position;
            }
            else if(handSO.side == HandSO.Side.Right)
            {
                placeToCardPosition = gameObject.transform.parent.transform.GetChild(2).transform.GetChild(1).transform.position;
            }

            return placeToCardPosition;
        }
    }

    //Animasyon baslangic ve bitis pozisyonlari icin local pozisyon alma islemi
    public Vector3 GetLocalPositionForAnimation()
    {
        if(currentStack.Count > 0)
        {
            _topDeckCard = currentStack.Peek();
            _topDeckCardPosition = _topDeckCard.transform.localPosition;
            _placeToCardHeightPosition = _topDeckCardPosition.y + 0.016f;
            placeToCardPosition = new Vector3(_topDeckCard.transform.localPosition.x, _placeToCardHeightPosition, 0);

            return placeToCardPosition;
        }
        else
        {
            if (handSO.side == HandSO.Side.Left)
            {
                placeToCardPosition = gameObject.transform.parent.transform.GetChild(2).transform.GetChild(0).transform.localPosition;
                placeToCardPosition = new Vector3(placeToCardPosition.x, placeToCardPosition.y, 0);
            }
            else if (handSO.side == HandSO.Side.Right)
            {
                placeToCardPosition = gameObject.transform.parent.transform.GetChild(2).transform.GetChild(1).transform.localPosition;
                placeToCardPosition = new Vector3(placeToCardPosition.x, placeToCardPosition.y, 0);
            }

            return placeToCardPosition;
        }
    }

    //Gate ve Rotator objelerine carpildigi durumda yapýlan islemler
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            if(currentStack.Count > 0)
            {
                //Matematisek hesaplamalar
                if (other.GetComponent<Gate>()._operator == "+")
                {
                    GetCardAndPlace(other.GetComponent<Gate>().value);
                }
                else if (other.GetComponent<Gate>()._operator == "-")
                {
                    RemoveCardFromDeck(other.GetComponent<Gate>().value);
                }
                else if (other.GetComponent<Gate>()._operator == "*")
                {
                    int result = currentStack.Count * (other.GetComponent<Gate>().value - 1);

                    GetCardAndPlace(result);
                }
            }
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Rotator"))
        {
            if(currentStack.Count > 0)
            {
                CameraController.Instance.Shake(0.5f, 2f);
                RemoveCardFromDeck(3);
                
            }
        }
        else if (other.CompareTag("Finish"))
        {
            PlayerMove.Instance.speed = 50f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            StartCoroutine(GameManager.Instance.CloseGame());
        }
    }
}
