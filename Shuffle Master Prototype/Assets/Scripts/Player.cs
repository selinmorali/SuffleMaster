using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 150f;
    private Rigidbody _rb;
    public static Player Instance;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(nameof(ForwardMove));
    }


    //Surekli one dogru player'in hareketini saglar
    IEnumerator ForwardMove()
    {
        yield return new WaitForSeconds(2.5f);
        while (true)
        {
            Vector3 _forwardMove = transform.forward * _speed * Time.deltaTime;
            _rb.MovePosition(_rb.position + _forwardMove);

            yield return new WaitForEndOfFrame();
        }
    }

    public void SetPlayerSpeed(float speed)
    {
        _speed = speed;
    }
}
