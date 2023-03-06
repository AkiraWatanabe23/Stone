using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text[] _turns = new Text[2];
    [SerializeField] private GameObject[] _thisTurn = new GameObject[2];
    [SerializeField] private Text _whichPlace = default;
    [SerializeField] private Text _winning = default;
    [SerializeField] private Image _moveSelect = default;
    [SerializeField] private Image _helpImage = default;

    private GameManager _manager = default;
    private Animator[] _anim = new Animator[2];

    public Text Winning { get => _winning; set => _winning = value; }
    public Image MoveSelect { get => _moveSelect; protected set => _moveSelect = value; }
    public Animator[] Anim { get => _anim; set => _anim = value; }

    private void Start()
    {
        _moveSelect.gameObject.SetActive(true);
        _helpImage.gameObject.SetActive(false);
        _manager = GetComponent<GameManager>();

        _anim[0] = _thisTurn[0].GetComponentInChildren<Animator>();
        _anim[1] = _thisTurn[1].GetComponentInChildren<Animator>();

        _anim[0].enabled = true;
        _anim[1].enabled = false;
    }

    private void Update()
    {
        _turns[0].text = "RED";
        _turns[1].text = "BLACK";

        if (_manager.Turn == Constants.Turns.RED)
        {
            _turns[0].color = Color.yellow;
            _turns[1].color = Color.white;
        }
        else if (_manager.Turn == Constants.Turns.BLACK)
        {
            _turns[0].color = Color.white;
            _turns[1].color = Color.yellow;
        }
        _whichPlace.gameObject.SetActive(!_moveSelect.gameObject.activeSelf);
    }
}
