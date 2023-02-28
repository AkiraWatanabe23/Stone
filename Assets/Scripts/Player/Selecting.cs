using Constants;
using System.Collections.Generic;
using UnityEngine;

public class Selecting : MonoBehaviour
{
    [SerializeField] private Material[] _default = new Material[3];

    private List<GameObject> _white = new();
    private List<GameObject> _black = new();
    /// <summary> マスを選択するときのVector </summary>
    private Vector3 _stonePos = Vector3.zero;
    /// <summary> 駒を選択するときのVector </summary>
    private Vector3 _piecePos = Vector3.up;
    private GameManager _manager = default;
    private readonly StoneSelect _stone = new();
    private readonly PieceSelect _piece = new();

    public bool IsSwitch { get; set; }
    public bool IsMovable { get; set; }
    public bool IsSelect { get; protected set; }

    private void Start()
    {
        _manager = GetComponent<GameManager>();
        _stone.Start(_manager, _default);
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
                SelectPiece();
                MovePiece();
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
                    _white.Add(piece);
                }
                else
                {
                    _manager.Board[(int)_stonePos.x][(int)_stonePos.z] = -1;
                    _black.Add(piece);
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
        if (IsMovable)
        {
            _piecePos = _piece.Select(Input.inputString, _piecePos);
            if (!IsSelect)
                IsSelect = true;
        }
    }

    /// <summary> 選んだ駒を移動させる </summary>
    private void MovePiece()
    {
        //TODO：駒の移動範囲の探索、描画（他のクラスでもいいかも）
    }

    private Vector3 SelectMovablePiece(int dir, Vector3 pos)
    {
        //内容は後で修正
        switch (dir)
        {
            //上方向
            case 0:
                if (pos.z + 1 > 4)
                    pos.z = 0;
                else
                    pos.z++;

                break;
            //下方向
            case 1:
                if (pos.z - 1 < 0)
                    pos.z = 4;
                else
                    pos.z--;

                break;
            //左方向
            case 2:
                if (pos.x - 1 < 0)
                    pos.x = 4;
                else
                    pos.x--;

                break;
            //右方向
            case 3:
                if (pos.x + 1 > 4)
                    pos.x = 0;
                else
                    pos.x++;

                break;
        }
        return pos;
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
