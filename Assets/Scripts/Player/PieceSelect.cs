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
        return pos;
    }
}
