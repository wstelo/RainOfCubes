using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer),typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    [SerializeField] private TimeViewer _viewer;

    private Material _material;
    private bool _isStartTime;
    private int _minDelay = 2;
    private int _maxDelay = 5;
    private Color _defaultColor;
    
    public event Action <Cube> EndedTimeToDestroy;
    public event Action ChangedTime;

    public int DelayToDestroy { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        Rigidbody = GetComponent<Rigidbody>();
        DelayToDestroy = UnityEngine.Random.Range(_minDelay, _maxDelay + 1);
        _defaultColor = _material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_isStartTime == false)
        {
            if (collision.gameObject.TryGetComponent(out Platform platform))
            {
                SetRandomColor();
                StartCoroutine(TimeCounting());
            }
        }
    }

    public void RefreshParameters()
    {
        Rigidbody.angularVelocity = Vector3.zero;
        Rigidbody.velocity = Vector3.zero;
        _viewer.RefreshTimer();
        DelayToDestroy = UnityEngine.Random.Range(_minDelay, _maxDelay + 1);
        _material.color = _defaultColor;
    }

    private IEnumerator TimeCounting()
    {
        _isStartTime = true;
        int currentTime = 0;
        int secondsCount = 1;
        var wait = new WaitForSeconds(secondsCount);

        _viewer.DisplayTime(currentTime, DelayToDestroy);

        while (currentTime < DelayToDestroy)
        {
            yield return wait;
            currentTime++;  
            _viewer.DisplayTime(currentTime,DelayToDestroy);
        }

        EndedTimeToDestroy?.Invoke(this);
        _isStartTime = false;
    }

    private void SetRandomColor()
    {
        _material.color = new Color (UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 255);
    }
}
