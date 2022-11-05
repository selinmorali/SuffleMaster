using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SwipeManager : MonoSingleton<SwipeManager>
{
    private Tween _tween;

    private void Start()
    {
        DOTween.SetTweensCapacity(250, 100);
    }

    //Eller arasi kart tasima islemi
    public void MoveCard(Hand startHand, Hand endHand, float distanceForCalcSpeed)
    {
        if (startHand.currentStack.Count > 0)
        {
            //Kart aktarim hizi icin cooldown
            StartCoroutine(CardMoveCooldown(distanceForCalcSpeed));

            GameObject _cardToMove = startHand.currentStack.Peek();
            startHand.currentStack.Pop();

            //Kartin tasinacagi pozisyonu belirler
            Vector3 _endPosition = StackManager.Instance.GetLocalPositionForNewCard(endHand);

            endHand.currentStack.Push(_cardToMove);

            _tween = _cardToMove.transform.DOLocalJump(_endPosition, 2f, 1, 0.3f);
        }
    }

    private IEnumerator CardMoveCooldown(float distance)
    {
        yield return new WaitForSeconds(CalculateSpeedCoef(distance));
    }

    //Surukleme mesafesi arttikca daha hizli kart transferi yapilmasi icin hiz hesaplamasi yapar.
    private float CalculateSpeedCoef(float distance)
    {
        float _speedCoef = 0;
        if (distance > 0)
        {
            if (distance <= 150)
            {
                _speedCoef = 0.015f;
            }
            else if (distance > 150 && distance <= 300)
            {
                _speedCoef = 0.01f;
            }
            else if (distance > 300 && distance <= 450)
            {
                _speedCoef = 0.0075f;
            }
        }
        else if (distance < 0)
        {
            if (distance >= -150)
            {
                _speedCoef = 0.015f;
            }
            else if (distance < -150 && distance >= -300)
            {
                _speedCoef = 0.01f;
            }
            else if (distance < -300 && distance >= -450)
            {
                _speedCoef = 0.0075f;
            }
        }
        return _speedCoef;
    }
}
