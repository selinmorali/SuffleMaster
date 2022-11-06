using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    public Transform Player;
    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - Player.position;
    }

    void Update()
    {
        SetCameraPosition();
    }

    //Player ile kamera arasindaki mesafeyi sabit tutmaya yarar   
    void SetCameraPosition()
    {
        transform.position = Player.position + _offset;
    }

    //Kameranin sallanmasini saglar
    private void OnShake(float duration, float strength)
    {
        transform.DOShakeRotation(duration, strength);
    }
    public void Shake(float duration, float strength) => OnShake(duration, strength);
}
