﻿using Constants;
using System.Collections.Generic;

[System.Serializable]
public class Judgment
{
    /// <summary>
    /// 横方向の判定
    /// </summary>
    public JudgeResult Row(List<int[]> board)
    {
        JudgeResult result = JudgeResult.DRAW;
        for (int i = 0; i < board.Count; i++)
        {
            string checking = string.Join(",", board[i]);

            if (checking == "1,1,1,1,1")
                result = JudgeResult.RED_WIN;
            else if (checking == "-1,-1,-1,-1,-1")
                result = JudgeResult.BLACK_WIN;

            if (result != JudgeResult.DRAW)
                break;
        }
        return result;
    }

    /// <summary>
    /// 縦方向の判定
    /// </summary>
    public JudgeResult Column(List<int[]> board)
    {
        JudgeResult result = JudgeResult.DRAW;
        for (int i = 0; i < board.Count; i++)
        {
            int pivot = 0;
            int count = 0;

            for (int j = 0; j < board.Count; j++)
            {
                var stone = board[j][i];

                if (pivot == 0)
                    pivot = stone;

                if (stone != 0 && pivot == stone)
                    count++;
                else
                    break;

                if (count == 5)
                {
                    if (stone == 1)
                    {
                        result = JudgeResult.RED_WIN;
                    }
                    else if (stone == -1)
                    {
                        result = JudgeResult.BLACK_WIN;
                    }
                }
            }
        }
        return result;
    }

    /// <summary>
    /// 斜め方向の判定
    /// </summary>
    public JudgeResult Diagonal(List<int[]> board)
    {
        JudgeResult result = JudgeResult.DRAW;
        bool[] dirs = { true, false };

        for (int i = 0; i < dirs.Length; i++)
        {
            int pivot = 0;
            int count = 0;
            int j =
                i == 0
                ? 0 : 4;
            int j_diff =
                i == 0
                ? 1 : -1;

            for (int k = 0; k < 5; k++)
            {
                var stone = board[i][j];

                if (pivot == 0)
                    pivot = stone;

                if (stone != 0 && stone == pivot)
                    count++;

                j += j_diff;
            }

            if (count == 5)
            {
                if (pivot == 1)
                {
                    result = JudgeResult.RED_WIN;
                }
                else if (pivot == -1)
                {
                    result = JudgeResult.BLACK_WIN;
                }
                break;
            }
        }
        return result;
    }
}
