using Constants;
using System.Collections.Generic;
using UnityEngine;

/// <summary> マスの判定(判定用のリストをつくり、判定して返す) </summary>
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
    public List<int[]> MovablePositions(List<int[]> current, GameObject piece, Turns turn)
    {
        //駒が移動する場合
        //1, ☑自分の周辺が0

        //駒の強化
        //2, □　　　　　　1 or -1（&& 自分が 1 or -1）

        //駒を奪う場合
        //3, ☑自分の周辺が敵駒 && その敵駒が自分より弱い

        List<int[]> checking = new();
        for (int i = 0; i < 5; i++)
        {
            checking.Add(new int[] { 0, 0, 0, 0, 0 });
        }
        var x = (int)piece.transform.position.x;
        var z = (int)piece.transform.position.z;
        var checkX = x;
        var checkZ = z;

        if (1 <= x && x <= 3 &&
            1 <= z && z <= 3)
        {
            //全方向の探索を行う場合
            for (int i = 0; i < 3; i++)
            {
                if (i == 1)
                    checkX++;
                else if (i == 2)
                    checkX -= 2;

                for (int j = 0; j < 3; j++)
                {
                    if (j == 1)
                        checkZ++;
                    else if (j == 2)
                        checkZ -= 2;

                    if (current[checkX][checkZ] == 0)
                        checking[checkX][checkZ] = 1;

                    //奪える駒の判定、駒の強化判定
                    if (turn == Turns.WHITE)
                    {
                        //そのマスに敵駒がいて、自分より弱い駒なら奪える
                        if (current[checkX][checkZ] < 0 &&
                            current[x][z] >= Mathf.Abs(current[checkX][checkZ]))
                        {
                            checking[checkX][checkZ] = 1;
                        }
                        //自分と移動先の駒が味方同士でかつ弱い駒（1）だったら移動して強化できる
                        if (current[x][z] == 1 &&
                            current[checkX][checkZ] == 1)
                        {
                            checking[checkX][checkZ] = 1;
                        }
                    }
                    else if (turn == Turns.BLACK)
                    {
                        if (current[checkX][checkZ] > 0 &&
                            Mathf.Abs(current[x][z]) >= current[checkX][checkZ])
                        {
                            checking[checkX][checkZ] = 1;
                        }

                        if (current[x][z] == -1 &&
                            current[checkX][checkZ] == -1)
                        {
                            checking[checkX][checkZ] = 1;
                        }
                    }
                }
                checkZ++;
            }
        }
        else
        {
            bool isHol = false;

            if (x == 0 || x == 4)
                isHol = true;
            else if (z == 0 || z == 4)
                isHol = false;

            //駒が端のラインにある場合
            for (int i = 0; i < 2; i++)
            {
                if (i == 1)
                {
                    if (isHol)
                    {
                        if (x == 0)
                            checkX++;
                        else if (x == 4)
                            checkX--;
                    }
                    else
                    {
                        if (z == 0)
                            checkZ++;
                        else if (z == 4)
                            checkZ--;
                    }
                }

                for (int j = 0; j < 3; j++)
                {
                    if (j == 1)
                    {
                        if (isHol)
                            checkZ++;
                        else
                            checkX++;
                    }
                    else if (j == 2)
                    {
                        if (isHol)
                            checkZ -= 2;
                        else
                            checkX -= 2;
                    }

                    if (0 <= z && z < current.Count)
                    {
                        if (current[checkX][checkZ] == 0)
                            checking[checkX][checkZ] = 1;
                    }

                    //奪える駒の判定
                    if (turn == Turns.WHITE)
                    {
                        //そのマスに敵駒がいて、自分より弱い駒なら
                        if (current[checkX][checkZ] < 0 &&
                            current[x][z] >= Mathf.Abs(current[checkX][checkZ]))
                        {
                            checking[checkX][checkZ] = 1;
                        }
                    }
                    else if (turn == Turns.BLACK)
                    {
                        if (current[checkX][checkZ] > 0 &&
                            Mathf.Abs(current[x][z]) >= current[checkX][checkZ])
                        {
                            checking[checkX][checkZ] = 1;
                        }
                    }
                }
                if (isHol)
                    checkZ++;
                else
                    checkX++;
            }
        }
        return checking;
    }
}
