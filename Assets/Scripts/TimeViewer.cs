using TMPro;
using UnityEngine;

public class TimeViewer : MonoBehaviour
{
    [SerializeField] Vector3 _offset;
    [SerializeField] private TMP_Text _timerText;

    private void Start()
    {
        RefreshTimer();
    }

    private void Update()
    {
        transform.position = transform.parent.position + _offset;
        transform.localRotation = Quaternion.Inverse(transform.parent.localRotation);
    }

    private void OnValidate()
    {
        transform.position = transform.parent.position + _offset;
    }

    public void RefreshTimer()
    {
        _timerText.text = $"";
    }

    public void DisplayTime(int currentTime, int delayToDestroy)
    {
        _timerText.text = $"{currentTime} / {delayToDestroy}";
    }
}
