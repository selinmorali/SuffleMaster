using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject LeftHand, RightHand;
    private int totalCardCount;
    public GameObject GameOverPanel;
    public bool isStarted = false;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        //Oyun basladiginda Game Over paneli kapali olmali
        CloseGameOverPanel();
    }


    //Ellerdeki kart sayilari sürekli olarak guncellenip kontrol edildigi kisim
    void Update()
    {
        //Ýki eldeki kartlari
        CheckTotalCardCount();

        if(isStarted && totalCardCount == 0)
        {
            StartCoroutine(EndGame());
        }
    }


    //Iki eldeki kart sayilarinin toplamini kontrol eder
    private void CheckTotalCardCount()
    {
        totalCardCount = LeftHand.GetComponent<StackController>().currentStack.Count + RightHand.GetComponent<StackController>().currentStack.Count;
    }


    //Oyunu bitirme islemi
    public IEnumerator EndGame()
    {
        OpenGameOverPanel();
        StopPlayerSpeed();
        yield return new WaitForSeconds(2);
        StopEditorApplication();
    }


    //Game Over panelini acar
    private void OpenGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }


    //GameOver panelini kapar
    private void CloseGameOverPanel()
    {
        GameOverPanel.SetActive(false);
    }


    //Player'in hizini durdurur
    private void StopPlayerSpeed()
    {
        PlayerMove.Instance.speed = 0f;
    }


    //Editor'un calismasini durdurur
    private void StopEditorApplication()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
