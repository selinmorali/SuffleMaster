using System.Collections.Generic;
using UnityEngine;

public class RightHand : Hand
{
    public static RightHand Instance;

    private void Awake()
    {
        Instance = this;
        currentStack = new Stack<GameObject>();
    }
    private void Start()
    {
        firstCardPosition = new Vector3(-0.00999999978f, 0.103f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            if (currentStack.Count > 0)
            {
                //Matematiksel hesaplamalar
                if (other.GetComponent<Gate>()._operator == "+")
                {
                    StackController.Instance.GetCardAndPlace(Instance, other.GetComponent<Gate>().value);
                }
                else if (other.GetComponent<Gate>()._operator == "-")
                {
                    StackController.Instance.RemoveCardFromDeck(Instance, other.GetComponent<Gate>().value);
                }
                else if (other.GetComponent<Gate>()._operator == "*")
                {
                    int result = currentStack.Count * (other.GetComponent<Gate>().value - 1);

                    StackController.Instance.GetCardAndPlace(Instance, result);
                }
            }
            other.gameObject.SetActive(false);
        }
        //Engele carpma
        else if (other.CompareTag("Rotator"))
        {
            if (currentStack.Count > 0)
            {
                CameraController.Instance.Shake(0.5f, 2f);
                StackController.Instance.RemoveCardFromDeck(Instance, 3);
            }
        }
        //Finish kismina ulasinca
        else if (other.CompareTag("Finish"))
        {
            Player.Instance.SetPlayerSpeed(50f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            StartCoroutine(GameManager.Instance.EndGame());
        }
    }
}
