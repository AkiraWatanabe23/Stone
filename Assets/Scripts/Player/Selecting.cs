using Constants;
using UnityEngine;

public class Selecting : MonoBehaviour
{
    [SerializeField] private Material[] _default = new Material[3];

    private int _index = 0;
    /// <summary> マスを選択するときのVector </summary>
    private Vector3 _stonePos = Vector3.zero;
    private GameManager _manager = default;
    private GameObject _selectedPiece = default;
    private readonly StoneSelect _stone = new();
    private readonly PieceSelect _piece = new();

    public GameObject SelectedPiece { get => _selectedPiece; protected set => _selectedPiece = value; }
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
                if (_selectedPiece == null)
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
                if (_manager.Turn == Turns.WHITE)
                {
                    _manager.Board[(int)_stonePos.x][(int)_stonePos.z] = 1;
                    _manager.White.Add(piece);
                }
                else
                {
                    _manager.Board[(int)_stonePos.x][(int)_stonePos.z] = -1;
                    _manager.Black.Add(piece);
                }

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

    /// <summary> 動かす駒を選ぶ（動かせる駒は探索済） </summary>
    private void SelectPiece()
    {
        if (Input.GetKeyDown(KeyCode.Return) && IsSelect)
        {
            _selectedPiece = _manager.Turn == Turns.WHITE ?
                             _manager.White[_index] : _manager.Black[_index];
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

    /// <summary> 選んだ駒を移動させる </summary>
    private void MovePiece()
    {
        //TODO：選んだ駒を指定したマスに移動させて、盤面情報を更新する
        if (Input.GetKeyDown(KeyCode.Return) && IsSelect)
        {
            if (_manager.CheckBoard[(int)_stonePos.x][(int)_stonePos.z] == 1)
            {
                //var piece = Instantiate(
                //    player, new Vector3((int)_stonePos.x, 1f, (int)_stonePos.z), Quaternion.identity);
                //if (_manager.Turn == Turns.WHITE)
                //{
                //    _manager.Board[(int)_stonePos.x][(int)_stonePos.z] = 1;
                //    _manager.White.Add(piece);
                //}
                //else
                //{
                //    _manager.Board[(int)_stonePos.x][(int)_stonePos.z] = -1;
                //    _manager.Black.Add(piece);
                //}

                Debug.Log("駒を移動しました");
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
