using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoneSelect
{
    [SerializeField] private Material[] _default = new Material[3];
    [SerializeField] private Material _selecting = default;

    private Vector3 _pos = Vector3.zero;
    private bool _isSelect = false;

    public GameObject Player { get; set; }
    public List<int[]> Board { get; set; }
    public List<GameObject[]> BoardState { get; set; }
    /// <summary> ターンの切り替え </summary>
    public bool IsSwitch { get; set; }

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            SetObj(Player);
        }
    }

    private void SetObj(GameObject player)
    {
        if (Input.GetKeyDown(KeyCode.Return) && _isSelect)
        {
            if (Board[(int)_pos.x][(int)_pos.z] == 1)
            {
                Object.Instantiate(
                    player, new Vector3((int)_pos.x, 1f, (int)_pos.z), Quaternion.identity);

                Debug.Log("マスを選択しました");
                IsSwitch = true;
            }
            else
            {
                Debug.Log("ここには置けないです");
                Debug.Log($"{Board[(int)_pos.x][(int)_pos.z]}, {new Vector3((int)_pos.x, 1f, (int)_pos.z)}");
            }
            _isSelect = false;
        }
        else
        {
            Select(Input.inputString);
            _isSelect = true;
        }
    }

    /// <summary> Playerの入力 </summary>
    /// <param name="input"> 入力されたキー </param>
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
        var mat = BoardState[(int)pos.x][(int)pos.z].GetComponent<MeshRenderer>().material;

        //Materialの描画
        if (mat.name.Contains("Orange"))
        {
            if (Board[(int)pos.x][(int)pos.z] == 1)
            {
                mat = _default[2];
            }
            else
            {
                mat = (int)(pos.x + pos.z) % 2 == 0 ? _default[0] : _default[1];
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

    /// <summary> ボードのリセット </summary>
    public void BoardFresh()
    {
        for (int i = 0; i < BoardState.Count; i++)
        {
            for (int j = 0; j < BoardState[i].Length; j++)
            {
                BoardState[i][j].GetComponent<MeshRenderer>().material
                    = (i + j) % 2 == 0 ? _default[0] : _default[1];
            }
        }
    }
}
