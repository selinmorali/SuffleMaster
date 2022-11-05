using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private float _startTouchPositionX, _previousTouchPositionX, _currentTouchPositionX;
    private float _deltaPositionX;
    private float _moveDistance;
    private float _oldDeltaPositionX;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            isTouched();
        }
        else
        {
            ResetPosition();
        }
    }

    //Dokunma islemleri
    private void isTouched()
    {
        switch (Input.GetTouch(0).phase)
        {
            case TouchPhase.Began:
                TouchBegan();
                break;

            case TouchPhase.Moved:
                TouchMoved();
                break;

            case TouchPhase.Stationary:
                TouchStationary();
                break;

            default:
                break;
        }
    }

    private void TouchBegan()
    {
        //Ekrana ilk dokundugum pozisyonun X degerini alir
        _startTouchPositionX = Input.GetTouch(0).position.x;
    }

    private void TouchMoved()
    {
        UpdateDeltaAndCurrentPosition();
        CheckDirectionChange();

        //Parmagi kaydirmaya devam ettigim surece bir ya da birkac frame onceki dokunma pozisyonu
        _previousTouchPositionX = _currentTouchPositionX;
        _moveDistance = _currentTouchPositionX - _startTouchPositionX;

        if (_moveDistance > 0 && LeftHand.Instance.currentStack.Count > 0)
        {
            SwipeToRight();
        }
        else if (_moveDistance < 0 && RightHand.Instance.currentStack.Count > 0)
        {
            SwipeToLeft();
        }
    }

    private void TouchStationary()
    {
        //10 degeri deadzone olarak belirlenmistir. Kaydirma islemi basladiktan sonra parmak basili kalirsa kart aktarim islemine devam etmek icin
        if (_moveDistance > 10 && LeftHand.Instance.currentStack.Count > 0)
        {
            SwipeToRight();
        }
        else if (_moveDistance < -10 && RightHand.Instance.currentStack.Count > 0)
        {
            SwipeToLeft();
        }
    }
   
    private void UpdateDeltaAndCurrentPosition()
    {
        //Parmagin ekranda dokundugu pozisyonun X degerini anlik olarak gunceller
        _currentTouchPositionX = Input.GetTouch(0).position.x;

        //Anlik olarak pozisyon degisikligini takip eder. Saga gidildiginde +, sola gidildiginde - deger tutar.
        _deltaPositionX = _currentTouchPositionX - _previousTouchPositionX;
    }

    //Parmak kaldirildiginda, ekranda bir dokunma durumu olmadiginda pozisyon degerlerini sifirlar.
    void ResetPosition()
    {
        _startTouchPositionX = 0;
        _currentTouchPositionX = 0;
        _previousTouchPositionX = 0;
        _moveDistance = 0;
    }

    //Soldan saga kaydirma islemi
    private void SwipeToRight()
    {
        //Soldan saga kaydirma yaparken elimde kart varsa
        if (LeftHand.Instance.currentStack.Count > 0)
        {
            SwipeManager.Instance.MoveCard(LeftHand.Instance, RightHand.Instance, _moveDistance);
        }
    }

    //Sagdan sola kaydirma islemi
    private void SwipeToLeft()
    {
        //Sagdan sola kaydirma yaparken elimde kart varsa
        if (RightHand.Instance.currentStack.Count > 0)
        {
            SwipeManager.Instance.MoveCard(RightHand.Instance, LeftHand.Instance, _moveDistance);
        }
    }

    //Kaydirma esnasindaki yon degisim kontrol islemi
    private void CheckDirectionChange()
    {
        //Bir onceki pozisyon degisikligi ile mevcut pozisyon degisikligi arasinda yon farkinin oldugunu anlamak icin (cunku saga gidiyorsa + sola gidiliyorsa - deger aldigini biliyoruz) bu degerleri carpiyoruz.
        if (_deltaPositionX * _oldDeltaPositionX < 0)
        {
            _startTouchPositionX = _currentTouchPositionX;
        }

        //Yon degigisikligini algilamak adina bir onceki frame'deki pozisyon farkliligini hafizada tutar.
        _oldDeltaPositionX = _deltaPositionX;
    }
}
