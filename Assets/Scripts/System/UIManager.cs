using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _turn = default;
    [SerializeField] private Text _whichPlace = default;
    [SerializeField] private Image _moveSelect = default;

    private GameManager _manager = default;

    public Image MoveSelect { get => _moveSelect; protected set => _moveSelect = value; }

    private void Start()
    {
        _manager = GetComponent<GameManager>();
        _moveSelect.gameObject.SetActive(true);
    }

    private void Update()
    {
        _turn.text = _manager.Turn.ToString();
        _whichPlace.gameObject.SetActive(!_moveSelect.gameObject.activeSelf);
    }
}
