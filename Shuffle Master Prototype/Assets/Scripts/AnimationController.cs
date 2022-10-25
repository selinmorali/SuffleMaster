using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationController : MonoBehaviour
{
    public PathType PathSystem = PathType.CatmullRom;
    public GameObject ActiveCardPool, PoolGO;
    public Vector3[] LeftPathValues = new Vector3[5];
    public Vector3[] RightPathValues = new Vector3[5];
    public Tween t;
    public Queue<GameObject> CardList;
    public GameObject leftHand;
    public GameObject rightHand;

    public static AnimationController Instance;

    private void Awake()
    {
        CardList = new Queue<GameObject>();
        Instance = this;
    }

    //Elden ele kartin tasinma animasyonu
    public void MoveCards(string side)
    {

        CardList.Enqueue(ObjectPooler.Instance.GetCard());
        CardList.Peek().SetActive(true);
        CardList.Peek().transform.parent = ActiveCardPool.transform;

        if (side == "ToRight")
        {
            leftHand.GetComponent<StackController>().RemoveCardFromDeck(1);

            LeftPathValues[0] = leftHand.GetComponent<StackController>().GetLocalPositionForAnimation();
            LeftPathValues[4] = rightHand.GetComponent<StackController>().GetLocalPositionForAnimation();
            CardList.Peek().transform.localPosition = LeftPathValues[0];
            t = CardList.Peek().transform.transform.DOLocalPath(LeftPathValues, 0.2f, PathSystem);

            t.SetEase(Ease.Linear).OnComplete(() =>
            {
                HideCard();
                rightHand.GetComponent<StackController>().GetCardAndPlace(1);
            });
        }
        else if(side == "ToLeft")
        {
            rightHand.GetComponent<StackController>().RemoveCardFromDeck(1);

            RightPathValues[0] = rightHand.GetComponent<StackController>().GetLocalPositionForAnimation();
            RightPathValues[4] = leftHand.GetComponent<StackController>().GetLocalPositionForAnimation();
            CardList.Peek().transform.localPosition = RightPathValues[0];
            t = CardList.Peek().transform.transform.DOLocalPath(RightPathValues, 0.2f, PathSystem);

            t.SetEase(Ease.Linear).OnComplete(() =>
            {
                HideCard();
                leftHand.GetComponent<StackController>().GetCardAndPlace(1);
            });

        }

    }

    //Animasyon tamamlandiginda animasyon kartini gizleme islemi
    private void HideCard()
    {
        CardList.Peek().transform.parent = PoolGO.transform;
        CardList.Peek().SetActive(false);
        CardList.Dequeue();
    }
}
