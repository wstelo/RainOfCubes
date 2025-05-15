using TMPro;
using UnityEngine;

public class TimeViewer : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Timer _timer;

    private void OnValidate()
    {
        transform.position = transform.parent.position + _offset;
    }

    private void Start()
    {
        RefreshTimer();
    }

    private void OnEnable()
    {
        _timer.ChangedTime += DisplayTime;
        _timer.TimeCountingEnded += RefreshTimer;
    }

    private void OnDisable()
    {
        _timer.ChangedTime -= DisplayTime;
        _timer.TimeCountingEnded += RefreshTimer;
    }

    private void Update()
    {
        transform.position = transform.parent.position + _offset;
        transform.localRotation = Quaternion.Inverse(transform.parent.localRotation);
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
