using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Vector2 _startTouchPosition, _currentTouchPosition;
    private float _deltaPosition;
    private int _speedCoef;
    [SerializeField] private GameObject leftHand, rightHand;

    void Update()
    {
        isTouched();
    }
                                                                              
    public void isTouched()
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

            if(_deltaPosition > 0)
            {
                leftHand.GetComponent<StackController>().RemoveCardFromDeck(1);
                rightHand.GetComponent<StackController>().GetCardAndPlace(1);
            }
            else if(_deltaPosition < 0)
            {
                rightHand.GetComponent<StackController>().RemoveCardFromDeck(1);
                leftHand.GetComponent<StackController>().GetCardAndPlace(1);
            }
        }
    }
}
