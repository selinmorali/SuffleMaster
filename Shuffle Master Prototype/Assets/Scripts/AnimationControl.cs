using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public GameObject card;
    [SerializeField] private Transform[] _routes;
    private int _routeToGo;
    private float _tParam;
    private Vector3 _cardPos;
    private float _speedModifier;
    private bool _coroutineAllowed;

    private void Start()
    {
        _routeToGo = 0;
        _tParam = 0f;
        _speedModifier = 0.5f;
        _coroutineAllowed = true;

        //objemi route'un ilk elemanýnýn pozisyonunda olusturdum.
        Instantiate(card, gameObject.transform.GetChild(0).GetChild(0).position, Quaternion.identity);
    }

    private IEnumerator GoByRoute(int routeNumber)
    {
        _coroutineAllowed = false;

        Vector3 p0 = _routes[routeNumber].GetChild(0).position;
        Vector3 p1 = _routes[routeNumber].GetChild(1).position;
        Vector3 p2 = _routes[routeNumber].GetChild(2).position;
        Vector3 p3 = _routes[routeNumber].GetChild(3).position;

        while (_tParam < 1)
        {
            _tParam += Time.deltaTime * _speedModifier;

            _cardPos = Mathf.Pow(1 - _tParam, 3) * p0 + 3 * Mathf.Pow(1 - _tParam, 2) * _tParam * p1 + 3 * (1 - _tParam) * Mathf.Pow(_tParam, 2) * p2 + Mathf.Pow(_tParam, 3) * p3;

            transform.position = _cardPos;
            yield return new WaitForEndOfFrame();
        }

        _tParam = 0f;
        _routeToGo += 1;

        if(_routeToGo > _routes.Length - 1)
        {
            _routeToGo = 0;
        }

        _coroutineAllowed = true;

    }

}


    

