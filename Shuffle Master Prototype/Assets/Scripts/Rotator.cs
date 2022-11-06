using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float speed = 3f;

    private void Start()
    {
        StartCoroutine(Rotate());
    }

    //Objenin kendi ekseninde donusunu saglar
    IEnumerator Rotate()
    {
        while (true)
        {
            transform.Rotate(0f, 0f, speed * Time.deltaTime / 0.01f, Space.Self);
            yield return new WaitForEndOfFrame();
        }
    }
}
