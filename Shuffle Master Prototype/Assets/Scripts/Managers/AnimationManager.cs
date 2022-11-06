using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class AnimationManager: MonoSingleton<AnimationManager>
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _handIcon;
    [SerializeField] private Ease _motionType;

    private void Start()
    {
        TutorialAnimation();
        MoveTutorialAnimationToUp();
    }

    //Egitim animasyonunu 2 saniye sonra yukari kaydirma islemi
    void MoveTutorialAnimationToUp()
    {
        UIManager.Instance.HoldAndDrag.transform.DOMoveY(2100f, 3f).SetDelay(2);
    }

    //Egitim animasyonu
    void TutorialAnimation()
    {
        _text.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(_motionType);
        _handIcon.transform.DOMoveX(400, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(_motionType);
    }

    //Kart boyutunun degistigi animasyon islemi
    public void CardScaleAnimation(GameObject card)
    {
        card.transform.DOScale(0.5f, 0.15f).SetLoops(2,LoopType.Yoyo).SetEase(Ease.OutFlash);
    }
}
