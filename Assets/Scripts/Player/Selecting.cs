using Constants;
using UnityEngine;

public class Selecting : MonoBehaviour
{
    [SerializeField] private Material[] _default = new Material[3];
    [SerializeField] private Material[] _pieceCol = new Material[2];

    private int _index = 0;
    /// <summary> マスを選択するときのVector </summary>
    private Vector3 _stonePos = Vector3.zero;
    private GameManager _manager = default;
    private readonly StoneSelect _stone = new();
    private readonly PieceSelect _piece = new();

    public Material[] PieceCol { get => _pieceCol; protected set => _pieceCol = value; }
    public GameObject SelectedPiece { get; protected set; }
    public bool IsSwitch { get; set; }
    public bool IsMovable { get; set; }
    public bool IsSelect { get; protected set; }

    private void Start()
    {
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
            SelectedPiece = _manager.Turn == Turns.WHITE ?
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
                _manager.Board[(int)pos.x][(int)pos.z] = 0;

                pos.x = _stonePos.x;
                pos.z = _stonePos.z;
                SelectedPiece.transform.position = pos;

                _manager.Board[(int)pos.x][(int)pos.z] =
                    _manager.Turn == Turns.WHITE ? 1 : -1;

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

    public void PieceFresh()
    {
        for (int i = 0; i < _manager.White.Count; i++)
        {
            Consts.FindWithVector(_manager.White[i].transform.position).
                GetComponent<MeshRenderer>().material = _pieceCol[0];
        }

        for (int i = 0; i < _manager.Black.Count; i++)
        {
            Consts.FindWithVector(_manager.Black[i].transform.position).
                GetComponent<MeshRenderer>().material = _pieceCol[1];
        }
    }
}
