using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI leftHandCount;
    public TextMeshProUGUI rightHandCount;
    public static UIController Instance;
    public GameObject leftHand;
    public GameObject rightHand;

    private void Awake()
    {
        Instance = this;
    }


    void Update()
    {
        //Kart sayisini surekli olarak gunceller.
        SetText();
    }


    //Ellerdeki kart sayilarinin ekranda gorunmesini saglar
    void SetText()
    {
        leftHandCount.text = leftHand.GetComponent<StackController>().currentStack.Count.ToString();
        rightHandCount.text = rightHand.GetComponent<StackController>().currentStack.Count.ToString();
    }
}
