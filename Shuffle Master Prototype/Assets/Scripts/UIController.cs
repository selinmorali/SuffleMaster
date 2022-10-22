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


        if (leftHand.GetComponent<StackController>().currentStack != null)
        {
            leftHandCount.text = leftHand.GetComponent<StackController>().currentStack.Count.ToString();
            rightHandCount.text = rightHand.GetComponent<StackController>().currentStack.Count.ToString();
        }
    }
}
