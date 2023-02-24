using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoneSelect
{
    [SerializeField] private Material[] _default = new Material[3];
    [SerializeField] private Material _selecting = default;

    private Vector3 _pos = Vector3.zero;

    public Vector3 Pos { get => _pos; set => _pos = value; }
    public List<int[]> Board { get; set; }
    public List<GameObject[]> BoardState { get; set; }

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            Select(Input.inputString);
        }
    }

    private void Select(string input)
    {
        switch (input)
        {
            case "w":
                Debug.Log("input up");
                _pos = MoveStone(0, _pos);
                break;
            case "s":
                Debug.Log("input down");
                _pos = MoveStone(1, _pos);
                break;
            case "a":
                Debug.Log("input left");
                _pos = MoveStone(2, _pos);
                break;
            case "d":
                Debug.Log("input right");
                _pos = MoveStone(3, _pos);
                break;
        }
    }

    /// <summary>
    /// マスの選択（描画の切り替え）
    /// </summary>
    private Vector3 MoveStone(int dir, Vector3 pos)
    {
        var mat = BoardState[(int)pos.x][(int)pos.z].GetComponent<MeshRenderer>().material;

        for (int i = 0; i < Board.Count; i++)
        {
            for (int j = 0; j < Board[i].Length; j++)
            {
                Debug.Log($"{Board[i][j]}, {i}, {j}");
            }
        }

        if (mat.name.Contains("Orange"))
        {
            if (Board[(int)pos.x][(int)pos.z] == 1)
            {
                mat = _default[2];
            }
            else
            {
                mat = (int)(pos.x + pos.z) % 2 == 0 ? _default[0] : _default[1];
                //Debug.Log("aaa");
            }
        }
        BoardState[(int)pos.x][(int)pos.z].GetComponent<MeshRenderer>().material = mat;

        switch (dir)
        {
            //上方向
            case 0:
                if (pos.z + 1 > 4)
                    pos.z = 0;
                else
                    pos.z++;

                BoardState[(int)pos.x][(int)pos.z].GetComponent<MeshRenderer>().material = _selecting;
                break;
            //下方向
            case 1:
                if (pos.z - 1 < 0)
                    pos.z = 4;
                else
                    pos.z--;

                BoardState[(int)pos.x][(int)pos.z].GetComponent<MeshRenderer>().material = _selecting;
                break;
            //左方向
            case 2:
                if (pos.x - 1 < 0)
                    pos.x = 4;
                else
                    pos.x--;

                BoardState[(int)pos.x][(int)pos.z].GetComponent<MeshRenderer>().material = _selecting;
                break;
            //右方向
            case 3:
                if (pos.x + 1 > 4)
                    pos.x = 0;
                else
                    pos.x++;

                BoardState[(int)pos.x][(int)pos.z].GetComponent<MeshRenderer>().material = _selecting;
                break;
        }
        return pos;
    }
}
