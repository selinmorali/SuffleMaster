using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 200f;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(nameof(ForwardMove));
    }

    IEnumerator ForwardMove()
    {
        yield return new WaitForSeconds(2.5f);
        while (true)
        {
            Vector3 _forwardMove = transform.forward * speed * Time.deltaTime;
            _rb.MovePosition(_rb.position + _forwardMove);

            yield return new WaitForEndOfFrame();
        }
    }
}
