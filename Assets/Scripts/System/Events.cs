using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    [Tooltip("ゲーム開始")]
    [SerializeField] private UnityEvent _startEvent = default;
    [Tooltip("UIを閉じる")]
    [SerializeField] private UnityEvent _closeEvent = default;
    [Tooltip("タイトルに戻る")]
    [SerializeField] private UnityEvent _toTitleEvent = default;

    private bool _isStart = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isStart)
        {
            _startEvent?.Invoke();
            Debug.Log("始めます");
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            _closeEvent?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) ||
            Input.GetKeyDown(KeyCode.RightControl))
        {
            Debug.Log("タイトルに戻ります");
            _toTitleEvent?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameClose();
        }
    }

    public void Startable()
    {
        _isStart = !_isStart;
    }

    private void GameClose()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
