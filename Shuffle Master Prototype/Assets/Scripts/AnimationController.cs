using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationController : MonoBehaviour
{
    private GameObject _card;
    public PathType PathSystem = PathType.CatmullRom;
    public GameObject LeftHandPool, RightHandPool, PoolGO;
    public Vector3[] PathValues = new Vector3[5];
    public Tween t;
    public Queue<GameObject> CardList;

    public static AnimationController Instance;

    private void Awake()
    {
        CardList = new Queue<GameObject>();
        Instance = this;
    }
    public void MoveCards(Vector3 firstPos, Vector3 lastPos, string side)
    {
        CardList.Enqueue(ObjectPooler.Instance.GetCard());
        CardList.Peek().SetActive(true);
        if(side == "Left")
        {
            CardList.Peek().transform.parent = LeftHandPool.transform;
        }
        else if(side == "Right")
        {
            CardList.Peek().transform.parent = RightHandPool.transform;
        }

        PathValues[0] = firstPos;
        PathValues[4] = lastPos;
        t = CardList.Peek().transform.transform.DOPath(PathValues, 2, PathSystem);

        t.SetEase(Ease.Linear).OnComplete(() =>
        {
            StartCoroutine(HideCard());
        });
    }

    IEnumerator HideCard()
    {
        yield return new WaitForSeconds(1f);
        CardList.Peek().transform.parent = PoolGO.transform;
        CardList.Peek().SetActive(false);
        CardList.Dequeue();
        yield return new WaitForEndOfFrame();
    }

}
