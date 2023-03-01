using Constants;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マスの判定(判定用のリストをつくり、判定して返す)
/// </summary>
[System.Serializable]
public class PlayableStones
{
    /// <summary> 配置可能なマスの判定
    ///           配置可→1,不可→0 </summary>
    public List<int[]> SettableStones(List<int[]> current)
    {
        List<int[]> checking = new();
        for (int i = 0; i < 5; i++)
        {
            checking.Add(new int[] { 0, 0, 0, 0, 0 });
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //そのマスが空なら
                if (current[i][j] == 0)
                {
                    checking[i][j] = 1;
                }
            }
        }
        return checking;
    }

    /// <summary> 移動可能な駒の判定
    ///           移動可→1,不可→0 </summary>
    public List<int[]> MovableStones(List<int[]> current, Turns turn)
    {
        List<int[]> checking = new();
        for (int i = 0; i < 5; i++)
        {
            checking.Add(new int[] { 0, 0, 0, 0, 0 });
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (turn == Turns.WHITE)
                {
                    if (current[i][j] == 1 ||
                        current[i][j] == 2)
                    {
                        checking[i][j] = 1;
                    }
                }
                else if (turn == Turns.BLACK)
                {
                    if (current[i][j] == -1 ||
                        current[i][j] == -2)
                    {
                        checking[i][j] = 1;
                    }
                }
            }
        }
        return checking;
    }

    /// <summary> 移動可能なマスの判定
    ///           移動可→1,不可→0 </summary>
    public List<int[]> MovablePositions(List<int[]> current, GameObject piece)
    {
        //駒の移動範囲の条件
        //1, 自分の周辺が0
        //2, 　　　　　　1 or -1
        //3, 　　　　　にいるものと符号が同じ(2 or -2)
        
        List<int[]> checking = new();
        for (int i = 0; i < 5; i++)
        {
            checking.Add(new int[] { 0, 0, 0, 0, 0 });
        }
        var x = (int)piece.transform.position.x;
        var z = (int)piece.transform.position.z;

        if (1 <= x && x <= 3 &&
            1 <= z && z <= 3)
        {
            //全方向の探索を行う場合
            for (int i = 0; i < 3; i++)
            {
                if (i == 1)
                    x++;
                else if (i == 2)
                    x -= 2;

                for (int j = 0; j < 3; j++)
                {
                    if (j == 1)
                        z++;
                    else if (j == 2)
                        z -= 2;

                    if (current[x][z] == 0)
                        checking[x][z] = 1;
                }
                z++;
            }
        }
        else
        {
            //駒が端のラインにある場合
            if (x == 0 || x == 4)
            {
                //左端、右端
                for (int i = 0; i < 2; i++)
                {
                    if (i == 1)
                    {
                        if (x == 0)
                            x++;
                        else if (x == 4)
                            x--;
                    }

                    for (int j = 0; j < 3; j++)
                    {
                        if (j == 1)
                            z++;
                        else if (j == 2)
                            z -= 2;

                        if (0 <= z && z < current.Count)
                        {
                            if (current[x][z] == 0)
                                checking[x][z] = 1;
                        }
                    }
                    z++;
                }
            }
            else if (z == 0 || z == 4)
            {
                ////下段、上段
                for (int i = 0; i < 2; i++)
                {
                    if (i == 1)
                    {
                        if (z == 0)
                            z++;
                        else if (z == 4)
                            z--;
                    }

                    for (int j = 0; j < 3; j++)
                    {
                        if (j == 1)
                            x++;
                        else if (j == 2)
                            x -= 2;

                        if (0 <= z && z < current.Count)
                        {
                            if (current[x][z] == 0)
                                checking[x][z] = 1;
                        }
                    }
                    x++;
                }
            }
        }
        return checking;
    }
}
