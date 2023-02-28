using Constants;
using UnityEngine;

[System.Serializable]
public class PieceSelect
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
                pos = SelectPiece(0, pos);
                break;

            case "s":
                pos = SelectPiece(1, pos);
                break;

            case "a":
                pos = SelectPiece(2, pos);
                break;

            case "d":
                pos = SelectPiece(3, pos);
                break;
        }
        return pos;
    }

    private Vector3 SelectPiece(int dir, Vector3 pos)
    {
        var piece = Consts.FindWithVector(pos);
        var mat = piece.GetComponent<MeshRenderer>().material;

        if (mat.name.Contains("Yellow"))
        {
            mat = _default[2];
        }
        piece.GetComponent<MeshRenderer>().material = mat;

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
}
