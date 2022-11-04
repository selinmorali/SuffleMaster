using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationController : MonoSingleton<AnimationController>
{
    public PathType PathSystem = PathType.CatmullRom;
    public GameObject ActiveCardPool, PoolGO;
    public Vector3[] LeftPathValues = new Vector3[5];
    public Vector3[] RightPathValues = new Vector3[5];
    public Tween t;
    public Queue<GameObject> ToRightQueue;
    public Queue<GameObject> ToLeftQueue;

    private void Awake()
    {
        ToLeftQueue = new Queue<GameObject>();
        ToRightQueue = new Queue<GameObject>();
    }

    //Elden ele kartin tasinma animasyonu
    public void MoveCardToRight()
    {
        ToRightQueue.Enqueue(ObjectPooler.Instance.GetCard());
        ToRightQueue.Peek().SetActive(true);
        ToRightQueue.Peek().transform.parent = ActiveCardPool.transform;

        //Eger kart tasima islemi saga dogruysa animasyon icin pozisyonlari alir ve animasyonu baslatir
            StackController.Instance.RemoveCardFromDeck(LeftHand.Instance,1);

            LeftPathValues[0] = StackController.Instance.GetLocalPositionForNewCard(LeftHand.Instance);
            LeftPathValues[4] = StackController.Instance.GetLocalPositionForNewCard(RightHand.Instance);
            ToRightQueue.Peek().transform.localPosition = LeftPathValues[0];
            StackController.Instance.GetCardAndPlace(RightHand.Instance,1);
            t = ToRightQueue.Peek().transform.transform.DOLocalPath(LeftPathValues, 0.20f, PathSystem);

            t.SetEase(Ease.Linear).OnComplete(() =>
            {
                //Animasyon tamamlandiginda animasyon kartini gizleme islemi
                ToRightQueue.Peek().transform.parent = PoolGO.transform;
                ToRightQueue.Peek().SetActive(false);
                ToRightQueue.Dequeue();
            });
    }

    public void MoveCardToLeft()
    {
        ToLeftQueue.Enqueue(ObjectPooler.Instance.GetCard());
        ToLeftQueue.Peek().SetActive(true);
        ToLeftQueue.Peek().transform.parent = ActiveCardPool.transform;

        //Eger kart tasima islemi sola dogruysa animasyon icin pozisyonlari alir ve animasyonu baslatir

        StackController.Instance.RemoveCardFromDeck(RightHand.Instance, 1);

        RightPathValues[0] = StackController.Instance.GetLocalPositionForNewCard(RightHand.Instance);
        RightPathValues[4] = StackController.Instance.GetLocalPositionForNewCard(LeftHand.Instance);
        ToLeftQueue.Peek().transform.localPosition = RightPathValues[0];
        StackController.Instance.GetCardAndPlace(LeftHand.Instance, 1);
        t = ToLeftQueue.Peek().transform.transform.DOLocalPath(RightPathValues, 0.20f, PathSystem);

        t.SetEase(Ease.Linear).OnComplete(() =>
        {
            //Animasyon tamamlandiginda animasyon kartini gizleme islemi
            ToLeftQueue.Peek().transform.parent = PoolGO.transform;
            ToLeftQueue.Peek().SetActive(false);
            ToLeftQueue.Dequeue();
        });
    }

}
