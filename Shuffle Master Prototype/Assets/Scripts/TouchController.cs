using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private float _lastTouchPosition, _currentTouchPosition;
    private float _deltaPosition;
    private float _speedCoef;
    [SerializeField] private GameObject leftHand, rightHand;
    private void Start()
    {
        StartCoroutine(isTouched());
    }

    IEnumerator isTouched()
    {
        while (true)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _lastTouchPosition = Input.GetTouch(0).position.x;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    _currentTouchPosition = Input.GetTouch(0).position.x;
                    //if(Input.GetTouch(0).deltaPosition.x )
                    //_deltaPosition = _currentTouchPosition.x - _startTouchPosition.x;
                    _deltaPosition = 5 * (Input.GetTouch(0).position.x - _lastTouchPosition) / Screen.width;
                    if (_currentTouchPosition > _lastTouchPosition)
                    {
                        Debug.Log("Yön Deðiþti.");
                    }
                    _lastTouchPosition = Input.GetTouch(0).position.x;
                    //_deltaPosition = Input.GetTouch(0).deltaPosition.x;
                    // _speedCoef = Mathf.Abs(_deltaPosition / 100);
                    if (_deltaPosition > 0 && leftHand.GetComponent<StackController>().currentStack.Count > 0)
                    {
                        yield return new WaitForSeconds(0.03f);
                        SwapToRight();
                    }
                    else if (_deltaPosition < 0 && rightHand.GetComponent<StackController>().currentStack.Count > 0)
                    {
                        yield return new WaitForSeconds(0.03f);
                        SwapToLeft();
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void SwapToRight()
    {
        if (leftHand.GetComponent<StackController>().currentStack.Count > 0)
        {
            leftHand.GetComponent<StackController>().RemoveCardFromDeck(1);
            rightHand.GetComponent<StackController>().GetCardAndPlace(1);
        }
    }

    private void SwapToLeft()
    {
        if (rightHand.GetComponent<StackController>().currentStack.Count > 0)
        {
            rightHand.GetComponent<StackController>().RemoveCardFromDeck(1);
            leftHand.GetComponent<StackController>().GetCardAndPlace(1);
        }
    }

}
