using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    [SerializeField] private UnityEvent _startEvent = default;
    [SerializeField] private UnityEvent _closeEvent = default;

    private bool _isStart = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isStart)
        {
            Debug.Log("始めます");
            _startEvent?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            _closeEvent?.Invoke();
        }
    }

    public void Startable()
    {
        _isStart = !_isStart;
    }
}
