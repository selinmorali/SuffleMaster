using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 _offset;
    public static CameraController Instance;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _offset = transform.position - player.position;
    }

    void Update()
    {
        //Player ile kamera arasindaki mesafeyi sabit tutmaya yarar
        transform.position = player.position + _offset;
    }

    //Kameranin sallanmasini saglar
    private void OnShake(float duration, float strength)
    {
        transform.DOShakeRotation(duration, strength);
    }

    public void Shake(float duration, float strength) => OnShake(duration, strength);
}
