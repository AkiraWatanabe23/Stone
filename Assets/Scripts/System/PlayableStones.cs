using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayableStones
{
    private List<string[]> _checking = new();

    public void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            _checking[i] = new string[] { "0", "0", "0", "0", "0"};
        }
    }

    /// <summary>
    /// 移動可能なマスの判定
    /// 既にあるものを動かすときに呼び出す
    /// </summary>
    public void MovableStones()
    {
        //1, 自分の周辺が0
        //2, 　　　　　　1 or -1
        //3, 　　　　　にいるものと符号が同じ(2 or -2)
    }

    /// <summary>
    /// 配置可能なマスの判定
    /// 新しく盤面に置くときに呼び出す
    /// </summary>
    public void SettableStones()
    {
        //0のマス
        //2, -2ではないマス
    }
}
