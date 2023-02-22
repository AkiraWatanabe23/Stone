using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoneSelect
{
    [SerializeField] private Material _selecting = default;

    public List<GameObject[]> Board { get; set; }

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
                MoveStone(0);
                break;
            case "d":
                Debug.Log("input down");
                MoveStone(1);
                break;
            case "a":
                Debug.Log("input left");
                MoveStone(2);
                break;
            case "s":
                Debug.Log("input right");
                MoveStone(3);
                break;
        }
    }

    /// <summary>
    /// マスの選択（描画の切り替え）
    /// </summary>
    /// <param name="dir"> 移動方向 </param>
    private void MoveStone(int dir)
    {
        Vector3 pos = Vector3.zero;

        switch (dir)
        {
            //上方向
            case 0:
                if (pos.z + 1 > 4)
                    pos.z = 0;
                else
                    pos.z++;

                Board[(int)pos.z][(int)pos.x].GetComponent<MeshRenderer>().material = _selecting;
                break;
            //下方向
            case 1:
                if (pos.z - 1 < 0)
                    pos.z = 4;
                else
                    pos.z--;

                Board[(int)pos.z][(int)pos.x].GetComponent<MeshRenderer>().material = _selecting;
                break;
            //左方向
            case 2:
                if (pos.x - 1 < 0)
                    pos.x = 4;
                else
                    pos.x--;

                Board[(int)pos.z][(int)pos.x].GetComponent<MeshRenderer>().material = _selecting;
                break;
            //右方向
            case 3:
                if (pos.x + 1 > 4)
                    pos.x = 0;
                else
                    pos.x++;

                Board[(int)pos.z][(int)pos.x].GetComponent<MeshRenderer>().material = _selecting;
                break;
        }
    }
}
