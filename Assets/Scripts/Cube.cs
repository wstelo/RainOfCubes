using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody),typeof(Timer))]

public class Cube : MonoBehaviour
{
    private Material _material;
    private bool _hasFirstCollision = false;
    private int _minDelay = 2;
    private int _maxDelay = 5;
    private Color _defaultColor;

    public event Action CollisionDetected;
    public event Action RefreshedParameters;

    public int DelayToDestroy { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Timer Timer { get; private set; }

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        Rigidbody = GetComponent<Rigidbody>();
        Timer = GetComponent<Timer>();
        DelayToDestroy = UnityEngine.Random.Range(_minDelay, _maxDelay + 1);
        _defaultColor = _material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform) && _hasFirstCollision == false)
        {
            _hasFirstCollision = true;
            SetRandomColor();
            CollisionDetected?.Invoke();
        }
    }

    public void RefreshParameters()
    {
        Rigidbody.angularVelocity = Vector3.zero;
        Rigidbody.velocity = Vector3.zero;
        DelayToDestroy = UnityEngine.Random.Range(_minDelay, _maxDelay + 1);
        _material.color = _defaultColor;
        _hasFirstCollision = false;
    }

    private void SetRandomColor()
    {
        _material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 255);
    }
}
