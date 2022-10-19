using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Vector2 _startTouchPosition, _endTouchPosition;
    private float _deltaPosition;
    private int _speedCoef;

    void Update()
    {
        isClick(); //týklanýp týklanmadýðýný sürekli update de kontrol edeceðim.
    }
                                                                              
    public void isClick()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startTouchPosition = Input.GetTouch(0).position; //dokunduðum andaki pozisyonumu deðiþkene eþitliyorum
        }

        //parmaðýmla bir yere týkladýysam ve parmaðýmý sürüklediysem
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _endTouchPosition = Input.GetTouch(0).position;
            _deltaPosition = _endTouchPosition.x - _startTouchPosition.x;
            _speedCoef = (int)Mathf.Abs(_deltaPosition / 100);
            Debug.Log(_speedCoef);
        }
    } 
}
