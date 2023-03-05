using Constants;
using UnityEngine;

[System.Serializable]
public class StoneSelect
{
    [SerializeField] private Material[] _default = new Material[3];

    private GameManager _manager = default;

    public void Start(GameManager manager, Material[] def)
    {
        _manager = manager;
        _default = def;
    }

    /// <summary> Playerの入力 </summary>
    public Vector3 Select(string input, Vector3 pos)
    {
        switch (input)
        {
            case "w":
                pos = SelectStone(0, pos);
                break;

            case "s":
                pos = SelectStone(1, pos);
                break;

            case "a":
                pos = SelectStone(2, pos);
                break;

            case "d":
                pos = SelectStone(3, pos);
                break;
        }
        return pos;
    }

    /// <summary> マスの選択（描画の切り替え） </summary>
    private Vector3 SelectStone(int dir, Vector3 pos)
    {
        var stone = Consts.FindWithVector(pos);
        var mat = stone.GetComponent<MeshRenderer>().material;

        //Materialの描画
        if (mat.name.Contains("Orange") || mat.name.Contains("Red"))
        {
            if (_manager.Move == MoveType.SET)
            {
                if (_manager.CheckBoard[(int)pos.x][(int)pos.z] == 1)
                    mat = _default[2];
                else
                    mat = (int)(pos.x + pos.z) % 2 == 0 ? _default[0] : _default[1];
            }
            else if (_manager.Move == MoveType.MOVE)
            {
                if (_manager.CheckBoard[(int)pos.x][(int)pos.z] == 1)
                    mat = _default[2];
                else
                    mat = (int)(pos.x + pos.z) % 2 == 0 ? _default[0] : _default[1];
            }

        }
        stone.GetComponent<MeshRenderer>().material = mat;

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
}
