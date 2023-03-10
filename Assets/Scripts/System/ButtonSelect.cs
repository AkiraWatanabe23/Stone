using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
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
