using System.Collections;
using UnityEngine;

public class Player : MonoSingleton<Player>
{
    private float _speed = 150f;
    private Rigidbody _rb;

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

    //Oyuncunun hizini degistirebilmemizi saglar
    public void SetPlayerSpeed(float speed)
    {
        _speed = speed;
    }
}
