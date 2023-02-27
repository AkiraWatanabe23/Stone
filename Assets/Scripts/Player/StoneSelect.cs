using Constants;
using UnityEngine;

public class StoneSelect : MonoBehaviour
{
    [SerializeField] private Material[] _default = new Material[3];

    private Vector3 _pos = Vector3.zero;
    private GameManager _manager = default;

    public Turns Turn { get; set; }
    public GameObject Player { get; set; }
    public Material Selecting { get; set; }
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
            if (Input.GetKeyDown(KeyCode.Return) && IsSelect)
            {
                if (_manager.Move == MoveType.SET)
                {
                    SetObj(Player);
                }
                else if (_manager.Move == MoveType.MOVE)
                {
                    SelectPiece();
                }
            }
            else if (IsMovable)
            {
                Select(Input.inputString);
                if (!IsSelect)
                {
                    IsSelect = true;
                }
            }
        }
    }

    /// <summary> 新しく駒を配置する場合 </summary>
    private void SetObj(GameObject player)
    {
        if (Input.GetKeyDown(KeyCode.Return) && IsSelect)
        {
            if (_manager.CheckBoard[(int)_pos.x][(int)_pos.z] == 1)
            {
                Instantiate(
                    player, new Vector3((int)_pos.x, 1f, (int)_pos.z), Quaternion.identity);
                //ここを変える
                _manager.Board[(int)_pos.x][(int)_pos.z] =
                    Turn == Turns.WHITE ? 1 : -1;

                Debug.Log("マスを選択しました");
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
            {
                IsSelect = true;
            }
        }
    }

    private void SelectPiece()
    {
        if (Input.GetKeyDown(KeyCode.Return) && IsSelect)
        {

        }
        else if (IsMovable)
        {
            Select(Input.inputString);
            if (!IsSelect)
            {
                IsSelect = true;
            }
        }
    }

    /// <summary> Playerの入力 </summary>
    private void Select(string input)
    {
        switch (input)
        {
            case "w":
                _pos = MoveStone(0, _pos);
                break;
            case "s":
                _pos = MoveStone(1, _pos);
                break;
            case "a":
                _pos = MoveStone(2, _pos);
                break;
            case "d":
                _pos = MoveStone(3, _pos);
                break;
        }
    }

    /// <summary> マスの選択（描画の切り替え） </summary>
    private Vector3 MoveStone(int dir, Vector3 pos)
    {
        string tag = Consts.STONE_TAG;
        var mat = Consts.FindWithVector(pos, tag).GetComponent<MeshRenderer>().material;

        //Materialの描画
        if (mat.name.Contains("Orange") || mat.name.Contains("Blue"))
        {
            if (_manager.CheckBoard[(int)pos.x][(int)pos.z] == 1)
            {
                mat = _default[2];
            }
            else
            {
                mat = (int)(pos.x + pos.z) % 2 == 0 ? _default[0] : _default[1];
            }
        }
        Consts.FindWithVector(pos, tag).GetComponent<MeshRenderer>().material = mat;

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
        Consts.FindWithVector(pos, tag).GetComponent<MeshRenderer>().material = Selecting;
        return pos;
    }

    /// <summary> ボードのリセット </summary>
    public void BoardFresh()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Consts.FindWithVector(new Vector3(i, 0, j), Consts.STONE_TAG).
                    GetComponent<MeshRenderer>().material
                    = (i + j) % 2 == 0 ? _default[0] : _default[1];
            }
        }
    }
}
