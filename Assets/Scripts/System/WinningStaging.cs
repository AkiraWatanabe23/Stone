using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
/// <summary> クリア時の演出 </summary>
public class WinningStaging
{
    [SerializeField] private Image _clear = default;
    [SerializeField] private UnityEvent _clearEvent = default;

    public void Start()
    {
        _clear.gameObject.SetActive(false);
    }

    public IEnumerator Winning()
    {
        _clear.gameObject.SetActive(true);

        yield return new WaitForSeconds(5);
        _clearEvent?.Invoke();
    }
}
