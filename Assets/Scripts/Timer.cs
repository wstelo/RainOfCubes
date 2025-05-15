using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class Timer : MonoBehaviour
{
    private Cube _cube;

    public event Action<Cube> EndedTimeToDestroy;
    public event Action<int,int> ChangedTime;
    public event Action TimeCountingEnded;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    private void OnEnable()
    {
        _cube.CollisionDetected += Enable;   
    }

    private void OnDisable()
    {
        _cube.CollisionDetected += Enable;
    }

    private void Enable()
    {
        StartCoroutine(TimeCounting());
    }

    private IEnumerator TimeCounting()
    {
        int currentTime = 0;
        int secondsCount = 1;
        var wait = new WaitForSeconds(secondsCount);

        ChangedTime?.Invoke(currentTime, _cube.DelayToDestroy);

        while (currentTime < _cube.DelayToDestroy)
        {
            yield return wait;
            currentTime++;
            ChangedTime?.Invoke(currentTime, _cube.DelayToDestroy);
        }

        EndedTimeToDestroy?.Invoke(_cube);
        TimeCountingEnded?.Invoke();
    }
}
