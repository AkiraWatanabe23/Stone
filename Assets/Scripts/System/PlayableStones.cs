using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マスの判定(判定用のリストをつくり、判定して返す)
/// </summary>
[System.Serializable]
public class PlayableStones
{
    /// <summary> 配置可能なマスの判定 </summary>
    public List<int[]> SettableStones(List<int[]> current)
    {
        List<int[]> checking = new();
        for (int i = 0; i < 5; i++)
        {
            checking.Add(new int[] { 1, 1, 1, 1, 1 });
        }

        for (int i = 0; i < current.Count; i++)
        {
            for (int j = 0; j < current[i].Length; j++)
            {
                //そのマスが空なら
                if (current[i][j] == 0)
                {
                    checking[i][j] = 0;
                }
            }
        }
        return checking;
        //配置可（空）     ...0
        //不可 （空でない）...1
    }

    /// <summary> 移動可能な駒の判定 </summary>
    public List<int[]> MovableStones(List<int[]> current, Turns turn)
    {
        List<int[]> checking = new();
        for (int i = 0; i < 5; i++)
        {
            checking.Add(new int[] { 1, 1, 1, 1, 1 });
        }

        for (int i = 0; i < current.Count; i++)
        {
            for (int j = 0; j < current[i].Length; j++)
            {
                if (turn == Turns.WHITE)
                {
                    if (current[i][j] == 1)
                    {
                        checking[i][j] = 0;
                    }
                }
                else
                {
                    if (current[i][j] == -1)
                    {
                        checking[i][j] = 0;
                    }
                }
            }
        }

        //1, 自分の周辺が0
        //2, 　　　　　　1 or -1
        //3, 　　　　　にいるものと符号が同じ(2 or -2)

        //移動可...0
        //不可　...1
        return checking;
    }
}
