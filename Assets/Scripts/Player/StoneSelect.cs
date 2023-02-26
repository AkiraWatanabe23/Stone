using Constants;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoneSelect
{
    [SerializeField] private Material[] _default = new Material[3];
    [SerializeField] private Material _selecting = default;

    private Vector3 _pos = Vector3.zero;

    public GameObject Player { get; set; }
    public List<int[]> Board { get; set; }
    public bool IsSwitch { get; set; }
    public bool IsMovable { get; set; }
    public bool IsSelect { get; protected set; }

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            SetObj(Player);
        }
    }

    private void SetObj(GameObject player)
    {
        if (Input.GetKeyDown(KeyCode.Return) && IsSelect)
        {
            if (Board[(int)_pos.x][(int)_pos.z] == 0)
            {
                Object.Instantiate(
                    player, new Vector3((int)_pos.x, 1f, (int)_pos.z), Quaternion.identity);
                //TODO：ここで、オブジェクト設定をボードに反映させる必要有
                Board[(int)_pos.x][(int)_pos.z] = 1;

                Debug.Log("マスを選択しました");
                IsSwitch = true;
                IsMovable = false;
            }
            else
            {
                Debug.Log("ここには置けないです");
            }
            IsSelect = false;
        }
        else if (IsMovable)
        {
            Select(Input.inputString);
            IsSelect = true;
        }
        else
        {
            Debug.Log(IsMovable);
            Debug.Log(IsSelect);
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
        var mat = Consts.FindWithVector(pos).GetComponent<MeshRenderer>().material;

        //Materialの描画
        if (mat.name.Contains("Orange"))
        {
            if (Board[(int)pos.x][(int)pos.z] == 0)
            {
                mat = _default[2];
            }
            else
            {
                mat = (int)(pos.x + pos.z) % 2 == 0 ? _default[0] : _default[1];
            }
        }
        Consts.FindWithVector(pos).GetComponent<MeshRenderer>().material = mat;

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
        Consts.FindWithVector(pos).GetComponent<MeshRenderer>().material = _selecting;
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
