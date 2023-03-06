using Constants;
using UnityEngine;

public class Selecting : MonoBehaviour
{
    [SerializeField] private Material[] _default = new Material[3];
    [Tooltip("オブジェクト生成時のSE")]
    [SerializeField] private AudioClip _setSound = default;

    private AudioSource _source = default;
    private int _index = 0;
    /// <summary> マスを選択するときのVector </summary>
    private Vector3 _stonePos = Vector3.zero;
    private GameManager _manager = default;
    private readonly StoneSelect _stone = new();
    private readonly PieceSelect _piece = new();

    public GameObject SelectedPiece { get; protected set; }
    public bool IsSwitch { get; set; }
    public bool IsMovable { get; set; }
    public bool IsSelect { get; protected set; }

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _manager = GetComponent<GameManager>();
        _stone.Start(_manager, _default);
        _piece.Start(_manager, _default);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (_manager.Move == MoveType.SET)
            {
                SetObj(_manager.Player);
            }
            else if (_manager.Move == MoveType.MOVE)
            {
                if (SelectedPiece == null)
                {
                    SelectPiece();
                }
                else
                {
                    MovePiece();
                }
            }
        }
    }

    /// <summary> 新しく駒を配置する場合 </summary>
    private void SetObj(GameObject player)
    {
        if (Input.GetKeyDown(KeyCode.Return) && IsSelect)
        {
            if (_manager.CheckBoard[(int)_stonePos.x][(int)_stonePos.z] == 1)
            {
                var piece = Instantiate(
                    player, new Vector3((int)_stonePos.x, 1f, (int)_stonePos.z), Quaternion.identity);
                _source.PlayOneShot(_setSound);
                _manager.PieceSetting(piece, _stonePos);

                Debug.Log("駒を配置しました");
                IsSwitch = true;
                IsMovable = false;
            }
            else
            {
                Debug.Log("このマスには置けないです");
            }
            IsSelect = false;
        }
        else if (IsMovable)
        {
            _stonePos = _stone.Select(Input.inputString, _stonePos);
            if (!IsSelect)
                IsSelect = true;
        }
    }

    /// <summary> 動かす駒を選ぶ </summary>
    private void SelectPiece()
    {
        if (Input.GetKeyDown(KeyCode.Return) && IsSelect)
        {
            SelectedPiece = _manager.Turn == Turns.RED ?
                            _manager.White[_index] : _manager.Black[_index];
            _manager.Movement(1);
        }
        else if (IsMovable)
        {
            Debug.Log("選択中..");
            _index =
                _piece.Select(Input.inputString, _index);
            if (!IsSelect)
                IsSelect = true;
        }
    }

    /// <summary> 選んだ駒を移動させて、盤面情報を更新する </summary>
    private void MovePiece()
    {
        if (Input.GetKeyDown(KeyCode.Return) && IsSelect)
        {
            if (_manager.CheckBoard[(int)_stonePos.x][(int)_stonePos.z] == 1)
            {
                var pos = SelectedPiece.transform.position;

                //移動先に敵の駒があれば、それを破壊し、駒数のカウントを減らす
                if (_manager.Board[(int)pos.x][(int)pos.z] *
                    _manager.Board[(int)_stonePos.x][(int)_stonePos.z] < 0)
                {
                    Debug.Log("敵駒を奪った");
                    Destroy(Consts.FindWithVector(new Vector3(pos.x, 1f, pos.z)));
                    _manager.CountDown(pos);
                }
                //移動して駒の強化が発生した場合、元のオブジェクトを破壊し、差し替える
                if (_manager.Board[(int)pos.x][(int)pos.z] ==
                    _manager.Board[(int)_stonePos.x][(int)_stonePos.z])
                {
                    Destroy(Consts.FindWithVector(
                        new Vector3(pos.x, 1f, pos.z)));
                    Destroy(Consts.FindWithVector(
                        new Vector3(_stonePos.x, 1f, _stonePos.z)));
                    Instantiate(
                        _manager.EnhancementPlayer,
                        new Vector3((int)_stonePos.x, 1f, (int)_stonePos.z),
                        Quaternion.identity);
                    Debug.Log("強化");
                }

                _manager.Board[(int)pos.x][(int)pos.z] = 0;

                pos.x = _stonePos.x;
                pos.z = _stonePos.z;
                SelectedPiece.transform.position = pos;

                _manager.Board[(int)pos.x][(int)pos.z] =
                    _manager.Turn == Turns.RED ? 1 : -1;

                Debug.Log("駒を移動しました");
                SelectedPiece = null;
                IsSwitch = true;
                IsMovable = false;
            }
            else
            {
                Debug.Log("このマスには置けないです");
            }
            IsSelect = false;
        }
        else if (IsMovable)
        {
            _stonePos = _stone.Select(Input.inputString, _stonePos);
            if (!IsSelect)
                IsSelect = true;
        }
    }

    /// <summary> ボードのリセット </summary>
    public void BoardFresh()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Consts.FindWithVector(new Vector3(i, 0, j)).
                    GetComponent<MeshRenderer>().material
                    = (i + j) % 2 == 0 ? _default[0] : _default[1];
            }
        }
    }
}
