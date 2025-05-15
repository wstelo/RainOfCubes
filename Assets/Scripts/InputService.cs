using UnityEngine;
using System;

public class InputService : MonoBehaviour
{
    public event Action ButtonClicked;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ButtonClicked?.Invoke();
        }
    }
}
