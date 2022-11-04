using UnityEngine;
using TMPro;

public class UIController : MonoSingleton<UIController>
{
    public TextMeshProUGUI leftHandCount;
    public TextMeshProUGUI rightHandCount;
    public GameObject GameOverPanel;

    void Update()
    {
        //Kart sayisini surekli olarak gunceller.
        SetText();
    }

    //Ellerdeki kart sayilarinin ekranda gorunmesini saglar
    private void SetText()
    {
        leftHandCount.text = LeftHand.Instance.currentStack.Count.ToString();
        rightHandCount.text = RightHand.Instance.currentStack.Count.ToString();
    }

    //Game Over panelini acar
    public void OpenGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }

    //GameOver panelini kapar
    public void CloseGameOverPanel()
    {
        GameOverPanel.SetActive(false);
    }

}
