using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キーボードによるUI操作
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private Selectable _startElement = default;

    private Selectable _currentElement = default;

    private void Start()
    {
        if (_startElement != null)
        {
            _currentElement = _startElement;
            _currentElement.Select();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_currentElement != null)
                _currentElement.OnDeselect(null);

            if (Input.GetKey(KeyCode.LeftShift) ||
                Input.GetKey(KeyCode.RightShift))
                _currentElement = _currentElement.FindSelectableOnUp();
            else
                _currentElement = _currentElement.FindSelectableOnDown();

            if (_currentElement != null)
                _currentElement.Select();
        }
    }
}
