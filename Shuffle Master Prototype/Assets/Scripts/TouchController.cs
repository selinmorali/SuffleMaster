using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Vector2 _startTouchPosition, _currentTouchPosition;
    private float _deltaPosition;
    private int _speedCoef;

    void Update()
    {
        isClick();
    }
                                                                              
    public void isClick()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startTouchPosition = Input.GetTouch(0).position; 
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _currentTouchPosition = Input.GetTouch(0).position;
            _deltaPosition = _currentTouchPosition.x - _startTouchPosition.x;
            _speedCoef = (int)Mathf.Abs(_deltaPosition / 100);
        }
    }
}
