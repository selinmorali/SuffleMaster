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
        isClick(); //tıklanıp tıklanmadığını sürekli update de kontrol edeceğim.
    }
                                                                              
    public void isClick()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startTouchPosition = Input.GetTouch(0).position; //dokunduğum andaki pozisyonumu değişkene eşitliyorum
        }

        //parmağımla bir yere tıkladıysam ve parmağımı sürüklediysem
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _endTouchPosition = Input.GetTouch(0).position;
            _deltaPosition = _endTouchPosition.x - _startTouchPosition.x;
            _speedCoef = (int)Mathf.Abs(_deltaPosition / 100);
            Debug.Log(_speedCoef);
        }
    } 
}
