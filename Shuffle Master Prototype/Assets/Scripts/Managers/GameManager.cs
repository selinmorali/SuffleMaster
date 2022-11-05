using System.Collections;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private int _totalCardCount;
    public bool IsStarted = false;

    private void Start()
    {
        IsStarted = true;
    }

    //Ellerdeki kart sayilari surekli olarak guncellenip kontrol edildigi kisim
    void Update()
    {
        //Iki eldeki kartlarin toplami
        CheckTotalCardCount();

        if(IsStarted && _totalCardCount == 0)
        {
            StartCoroutine(EndGame());
        }
    }

    //Iki eldeki kart sayilarinin toplamini kontrol eder
    private void CheckTotalCardCount()
    {
        _totalCardCount = LeftHand.Instance.currentStack.Count + RightHand.Instance.currentStack.Count;
    }

    //Oyunu bitirme islemi
    public IEnumerator EndGame()
    {
        UIManager.Instance.OpenGameOverPanel();
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
