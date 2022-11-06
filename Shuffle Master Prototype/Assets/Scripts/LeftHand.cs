using System.Collections.Generic;
using UnityEngine;

public class LeftHand : Hand
{
    public static LeftHand Instance;

    private void Awake()
    {
        Instance = this;
        currentStack = new Stack<GameObject>();      
        firstCardPosition = new Vector3(0.733f, 0.110f, -0.113f); //Elde hic kart yokken bu ele gelecek ilk kartin yerlesecegi pozisyon.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            if (currentStack.Count > 0)
            {
                //Matematiksel hesaplamalar
                switch (other.GetComponent<Gate>().SO.Operator)
                {
                    case GateSO.OperatorType.Sum:
                        StackManager.Instance.GetCardAndPlace(Instance, other.GetComponent<Gate>().SO.Value);
                        break;
                    case GateSO.OperatorType.Sub:
                        StackManager.Instance.RemoveCardFromDeck(Instance, other.GetComponent<Gate>().SO.Value);
                        break;
                    case GateSO.OperatorType.Multiply:
                        int result = currentStack.Count * (other.GetComponent<Gate>().SO.Value - 1);
                        StackManager.Instance.GetCardAndPlace(Instance, result);
                        break;
                    default:
                        break;
                }
            }
            other.gameObject.SetActive(false);
        }
        //Engele carpma
        else if (other.CompareTag("Rotator"))
        {
            if (currentStack.Count > 0)
            {
                CameraManager.Instance.Shake(0.5f, 2f);
                StackManager.Instance.RemoveCardFromDeck(Instance, 3);
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
        //Finish cizgisinden cikildiginda
        if (other.CompareTag("Finish"))
        {
            StartCoroutine(GameManager.Instance.EndGame());
        }
    }
}
