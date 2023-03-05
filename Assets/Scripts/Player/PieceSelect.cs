using Constants;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[System.Serializable]
public class PieceSelect
{
    [SerializeField] private Material[] _default = new Material[3];

    private GameManager _manager = default;

    public void Start(GameManager manager, Material[] mat)
    {
        _manager = manager;
        _default = mat;
    }

    /// <summary> Playerの入力 </summary>
    public int Select(string input, int index)
    {
        int num = 0;
        switch (input)
        {
            case "w":
            case "s":
            case "a":
            case "d":
                num = SelectPiece(index);
                break;
        }
        return num;
    }

    private int SelectPiece(int index)
    {
        Vector3 stonePos = default;
        if (_manager.Turn == Turns.WHITE)
        {
            stonePos = _manager.White[index].transform.position;
        }
        else if (_manager.Turn == Turns.BLACK)
        {
            stonePos = _manager.Black[index].transform.position;
        }
        var mat = Consts.FindWithVector(new Vector3((int)stonePos.x, 0f, (int)stonePos.z)).
            GetComponent<MeshRenderer>().material;
        if (mat.name.Contains("Orange") || mat.name.Contains("Blue"))
        {
            mat = (int)(stonePos.x + stonePos.z) % 2 == 0 ? _default[0] : _default[1];
        }
        Consts.FindWithVector(new Vector3((int)stonePos.x, 0f, (int)stonePos.z)).
            GetComponent<MeshRenderer>().material = mat;


        if (_manager.Turn == Turns.WHITE)
        {
            if (index + 1 <= _manager.White.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }
        else if (_manager.Turn == Turns.BLACK)
        {
            if (index + 1 <= _manager.Black.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }

        if (_manager.Turn == Turns.WHITE)
        {
            var pos = _manager.White[index].transform.position;
            Consts.FindWithVector(new Vector3((int)pos.x, 0f, (int)pos.z)).
                GetComponent<MeshRenderer>().material = _manager.Selecting;

        }
        else if (_manager.Turn == Turns.BLACK)
        {
            var pos = _manager.Black[index].transform.position;
            Consts.FindWithVector(new Vector3((int)pos.x, 0f, (int)pos.z)).
                GetComponent<MeshRenderer>().material = _manager.Selecting;
        }
        return index;
    }
}
