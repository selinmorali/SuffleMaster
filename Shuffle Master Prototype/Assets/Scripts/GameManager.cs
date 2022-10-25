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
        GameOverPanel.SetActive(false);
    }

    void Update()
    {
        totalCardCount = LeftHand.GetComponent<StackController>().currentStack.Count + RightHand.GetComponent<StackController>().currentStack.Count;

        if(isStarted && totalCardCount == 0)
        {
            StartCoroutine(nameof(CloseGame));
        }
    }

    public IEnumerator CloseGame()
    {
        GameOverPanel.SetActive(true);
        PlayerMove.Instance.speed = 0f;
        yield return new WaitForSeconds(2);
        UnityEditor.EditorApplication.isPlaying = false;  
    }
}
