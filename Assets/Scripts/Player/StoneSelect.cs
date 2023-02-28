using Constants;
using UnityEngine;

public class StoneSelect : MonoBehaviour
{
    [SerializeField] private Material[] _default = new Material[3];

    /// <summary> マスを選択するときのVector </summary>
    private Vector3 _stonePos = Vector3.zero;
    /// <summary> 駒を選択するときのVector </summary>
    private Vector3 _piecePos = Vector3.up;
    private GameManager _manager = default;

    public bool IsSwitch { get; set; }
    public bool IsMovable { get; set; }
    public bool IsSelect { get; protected set; }

    private void Start()
    {
        _manager = GetComponent<GameManager>();
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
                Instantiate(
                    player, new Vector3((int)_stonePos.x, 1f, (int)_stonePos.z), Quaternion.identity);
                _manager.Board[(int)_stonePos.x][(int)_stonePos.z] =
                    _manager.Turn == Turns.WHITE ? 1 : -1;

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
            Select(Input.inputString);
            if (!IsSelect)
                IsSelect = true;
        }
    }

    /// <summary> 動かす駒を選ぶ（動かせる駒は探索済） </summary>
    private void SelectPiece()
    {
        if (IsMovable)
        {
            Select(Input.inputString);
            if (!IsSelect)
                IsSelect = true;
        }
    }

    /// <summary> 選んだ駒を移動させる </summary>
    private void MovePiece()
    {
        //TODO：駒の移動範囲の探索、描画（他のクラスでもいいかも）
    }

    /// <summary> Playerの入力 </summary>
    private void Select(string input)
    {
        switch (input)
        {
            case "w":
                if (_manager.Move == MoveType.SET)
                    _stonePos = SelectStone(0, _stonePos);
                else if (_manager.Move == MoveType.MOVE)
                    _piecePos = SelectMovablePiece(0, _piecePos);
                break;

            case "s":
                if (_manager.Move == MoveType.SET)
                    _stonePos = SelectStone(1, _stonePos);
                else if (_manager.Move == MoveType.MOVE)
                    _piecePos = SelectMovablePiece(1, _piecePos);
                break;

            case "a":
                if (_manager.Move == MoveType.SET)
                    _stonePos = SelectStone(2, _stonePos);
                else if (_manager.Move == MoveType.MOVE)
                    _piecePos = SelectMovablePiece(2, _piecePos);
                break;

            case "d":
                if (_manager.Move == MoveType.SET)
                    _stonePos = SelectStone(3, _stonePos);
                else if (_manager.Move == MoveType.MOVE)
                    _piecePos = SelectMovablePiece(3, _piecePos);
                break;
        }
    }

    /// <summary> マスの選択（描画の切り替え） </summary>
    private Vector3 SelectStone(int dir, Vector3 pos)
    {
        var mat =
            Consts.FindWithVector(pos).GetComponent<MeshRenderer>().material;

        //Materialの描画
        if (mat.name.Contains("Orange") || mat.name.Contains("Blue"))
        {
            if (_manager.CheckBoard[(int)pos.x][(int)pos.z] == 1)
                mat = _default[2];
            else
                mat = (int)(pos.x + pos.z) % 2 == 0 ? _default[0] : _default[1];
        }
        Consts.FindWithVector(pos).
            GetComponent<MeshRenderer>().material = mat;

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
        Consts.FindWithVector(pos).
            GetComponent<MeshRenderer>().material = _manager.Selecting;
        return pos;
    }

    private Vector3 SelectMovablePiece(int dir, Vector3 pos)
    {
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
