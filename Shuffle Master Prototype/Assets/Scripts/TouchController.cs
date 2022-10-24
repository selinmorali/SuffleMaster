using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private float _startTouchPositionX, _previousTouchPositionX, _currentTouchPositionX;
    private float _deltaPositionX;
    private float _moveDistance;
    private float _oldDeltaPositionX;
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
                    //Ekrana ilk dokundugum pozisyonun X degerini alir.
                    _startTouchPositionX = Input.GetTouch(0).position.x;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    //Parmagin ekranda dokundugu pozisyonun X degerini anlik olarak gunceller.
                    _currentTouchPositionX = Input.GetTouch(0).position.x;

                    //Anlik olarak pozisyon degisikligini takip eder. Saga gidildiginde +, sola gidildiginde - deger tutar.
                    _deltaPositionX = _currentTouchPositionX - _previousTouchPositionX;

                    //hizli bir sekilde parmak kaydirildiginda deltaPositionX degerini dogru guncellemek icin ufak bir cooldown.
                    yield return new WaitForSeconds(0.0005f);
                    CheckDirectionChange();

                    //Parmagi kaydirmaya devam ettigim surece bir ya da birkac frame onceki dokunma pozisyonu.
                    _previousTouchPositionX = _currentTouchPositionX;
                     _moveDistance = _currentTouchPositionX - _startTouchPositionX;

                    if (_moveDistance > 0 && leftHand.GetComponent<StackController>().currentStack.Count > 0)
                    {
                        //Kart aktarim hizi icin cooldown
                        yield return new WaitForSeconds(CalculateSpeedCoef(_moveDistance));
                        SwapToRight();
                    }
                    else if (_moveDistance < 0 && rightHand.GetComponent<StackController>().currentStack.Count > 0)
                    {
                        //Kart aktarim hizi icin cooldown
                        yield return new WaitForSeconds(CalculateSpeedCoef(_moveDistance));
                        SwapToLeft();
                    }
                }
                else if(Input.GetTouch(0).phase == TouchPhase.Stationary)
                {
                    //10 degeri deadzone olarak belirlenmistir. Kaydirma islemi basladiktan sonra parmak basili kalirsa
                    //kart aktarim islemine devam etmek icin.
                    if (_moveDistance > 10 && leftHand.GetComponent<StackController>().currentStack.Count > 0)
                    {
                        yield return new WaitForSeconds(CalculateSpeedCoef(_moveDistance));
                        SwapToRight();
                    }
                    else if (_moveDistance < -10 && rightHand.GetComponent<StackController>().currentStack.Count > 0)
                    {
                        yield return new WaitForSeconds(CalculateSpeedCoef(_moveDistance));
                        SwapToLeft();
                    }
                }
            }
            else
            {
                //Parmak kaldirildiginda, ekranda bir dokunma durumu olmadiginda pozisyon degerlerini sifirlar.
                _startTouchPositionX = 0;
                _currentTouchPositionX = 0;
                _previousTouchPositionX = 0;
                _moveDistance = 0;
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

    private void CheckDirectionChange()
    {
        //Bir onceki pozisyon degisikligi ile mevcut pozisyon degisikligi arasinda yon farkinin oldugunu
        //anlamak icin (cunku saga gidiyorsa + sola gidiliyorsa - deger aldigini biliyoruz) bu degerleri carpiyoruz.
        if (_deltaPositionX * _oldDeltaPositionX < 0)
        {
            _startTouchPositionX = _currentTouchPositionX;
        }

        //Yon degigisikligini algilamak adina bir onceki frame'deki pozisyon farkliligini hafizada tutar.
        _oldDeltaPositionX = _deltaPositionX;
    }

    private float CalculateSpeedCoef(float distance)
    {
        if(distance > 0)
        {
            if(distance <= 150)
            {
                _speedCoef = 0.04f;
            }
            else if(distance > 150 && distance <= 300)
            {
                _speedCoef = 0.03f;
            }
            else if(distance > 300 && distance <= 450)
            {
                _speedCoef = 0.02f;
            }
            else if(distance > 450 && distance <= 600)
            {
                _speedCoef = 0.01f;
            }
        }
        else if(distance < 0)
        {
            if (distance >= -150)
            {
                _speedCoef = 0.04f;
            }
            else if (distance < -150 && distance >= -300)
            {
                _speedCoef = 0.03f;
            }
            else if (distance < -300 && distance >= -450)
            {
                _speedCoef = 0.02f;
            }
            else if (distance < -450 && distance >= -600)
            {
                _speedCoef = 0.01f;
            }
        }
        return _speedCoef;
    }

}
