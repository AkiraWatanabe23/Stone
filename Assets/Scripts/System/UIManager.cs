using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _turn = default;
    [SerializeField] private Text _whichPlace = default;
    [SerializeField] private Image _moveSelect = default;
    [SerializeField] private Selectable _startElement = default;

    private Selectable _currentElement = default;
    private GameManager _manager = default;

    public Image MoveSelect { get => _moveSelect; protected set => _moveSelect = value; }

    void Start()
    {
        _manager = GetComponent<GameManager>();
        _moveSelect.gameObject.SetActive(true);

        if (_startElement != null)
        {
            _currentElement = _startElement;
            _currentElement.Select();
        }
    }

    private void Update()
    {
        _turn.text = _manager.Turn.ToString();
        _whichPlace.gameObject.SetActive(!_moveSelect.gameObject.activeSelf);

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
