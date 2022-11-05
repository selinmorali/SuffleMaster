using UnityEngine;

[CreateAssetMenu(fileName = "GateSO", menuName = "ScriptableObjects/GateSO", order = 1)]
public class GateSO : ScriptableObject
{
    //Kapinin uzerindeki matematiksel islem operatorunu ve islem yapilacak degeri tutar
    
    public enum OperatorType
    {
        Sum,
        Multiply,
        Sub
    }

    public OperatorType Operator;
    public int Value;
}
