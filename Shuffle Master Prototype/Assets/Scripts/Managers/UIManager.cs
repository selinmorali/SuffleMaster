using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public TextMeshProUGUI LeftHandCount;
    public TextMeshProUGUI RightHandCount;
    public GameObject GameOverPanel;

    void Update()
    {
        //Kart sayisini surekli olarak gunceller.
        SetText();
    }

    //Ellerdeki kart sayilarinin ekranda gorunmesini saglar
    private void SetText()
    {
        LeftHandCount.text = LeftHand.Instance.currentStack.Count.ToString();
        RightHandCount.text = RightHand.Instance.currentStack.Count.ToString();
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
