using System.Collections;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private int totalCardCount;
    public bool isStarted = false;

    private void Start()
    {
        isStarted = true;
    }

    //Ellerdeki kart sayilari surekli olarak guncellenip kontrol edildigi kisim
    void Update()
    {
        //Iki eldeki kartlarin toplami
        CheckTotalCardCount();

        if(isStarted && totalCardCount == 0)
        {
            StartCoroutine(EndGame());
        }
    }

    //Iki eldeki kart sayilarinin toplamini kontrol eder
    private void CheckTotalCardCount()
    {
        totalCardCount = LeftHand.Instance.currentStack.Count + RightHand.Instance.currentStack.Count;
    }

    //Oyunu bitirme islemi
    public IEnumerator EndGame()
    {
        UIController.Instance.OpenGameOverPanel();
        Player.Instance.SetPlayerSpeed(0);
        yield return new WaitForSeconds(2);
        StopEditorApplication();
    }

    //Editor'un calismasini durdurur
    private void StopEditorApplication()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
