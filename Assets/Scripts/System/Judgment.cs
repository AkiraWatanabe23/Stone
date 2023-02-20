using Constants;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 勝利判定
/// </summary>
public class Judgment : MonoBehaviour
{
    private List<string[]> _board = new();

    /// <summary>
    /// 横方向の判定
    /// </summary>
    private JudgeResult Row()
    {
        JudgeResult result = JudgeResult.DRAW;
        for (int i = 0; i < _board.Count; i++)
        {
            string checking = string.Join(",", _board[i]);

            if (checking == "1,1,1,1,1")
                result = JudgeResult.WHITE_WIN;
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
    private JudgeResult Column()
    {
        JudgeResult result = JudgeResult.DRAW;
        for (int i = 0; i < _board.Count; i++)
        {
            string pivot = " ";
            int count = 0;

            for (int j = 0; j < _board.Count; j++)
            {
                var stone = _board[j][i];

                if (pivot == " ")
                    pivot = stone;

                if (stone != "0" && pivot == stone)
                    count++;
                else
                    break;

                if (count == 5)
                {
                    if (stone == "1")
                    {
                        result = JudgeResult.WHITE_WIN;
                    }
                    else if (stone == "-1")
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
    private JudgeResult Diagonal()
    {
        JudgeResult result = JudgeResult.DRAW;
        bool[] dirs = { true, false };

        for (int i = 0; i < dirs.Length; i++)
        {
            string pivot = " ";
            int count = 0;
            int j =
                i == 0
                ? 0 : 4;
            int j_diff =
                i == 0
                ? 1 : -1;

            for (int k = 0; k < 5; k++)
            {
                var stone = _board[i][j];

                if (pivot == " ")
                    pivot = stone;

                if (stone != "0" && stone == pivot)
                    count++;

                j += j_diff;
            }

            if (count == 5)
            {
                if (pivot == "1")
                {
                    result = JudgeResult.WHITE_WIN;
                }
                else if (pivot == "-1")
                {
                    result = JudgeResult.BLACK_WIN;
                }
                break;
            }
        }
        return result;
    }
}
