using Constants;
using UnityEngine;

public class Judgment : MonoBehaviour
{
    private string[] _board = new string[5];

    /// <summary>
    /// 横方向の判定
    /// </summary>
    /// <returns> 判定結果 </returns>
    private JudgeResult Row()
    {
        JudgeResult result = JudgeResult.DRAW;
        for (int i = 0; i < _board.Length; i++)
        {
            if (_board[i] == "OOOOO")
                result = JudgeResult.WHITE_WIN;
            else if (_board[i] == "XXXXX")
                result = JudgeResult.BLACK_WIN;
        }

        return result;
    }

    /// <summary>
    /// 縦方向の判定
    /// </summary>
    /// <returns> 判定結果 </returns>
    private JudgeResult Column()
    {
        JudgeResult result = JudgeResult.DRAW;
        for (int i = 0; i < _board.Length; i++)
        {
            char pivot = ' ';
            int count = 0;

            for (int j = 0; j < _board.Length; j++)
            {
                var stone = _board[j][i];

                if (pivot == ' ')
                    pivot = stone;

                if (stone != '.' && pivot == stone)
                    count++;
                else
                    break;

                if (count == 5)
                {
                    if (stone == 'O')
                    {
                        result = JudgeResult.WHITE_WIN;
                    }
                    else if (stone == 'X')
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
    /// <returns> 判定結果 </returns>
    private JudgeResult Diagonal()
    {
        JudgeResult result = JudgeResult.DRAW;
        bool[] dirs = new bool[] { true, false };

        for (int i = 0; i < dirs.Length; i++)
        {
            char pivot = ' ';
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

                if (pivot == ' ')
                    pivot = stone;

                if (stone != '.' && stone == pivot)
                    count++;

                j += j_diff;
            }

            if (count == 5)
            {
                if (pivot == 'O')
                {
                    result = JudgeResult.WHITE_WIN;
                }
                else if (pivot == 'X')
                {
                    result = JudgeResult.BLACK_WIN;
                }
                break;
            }
        }

        return result;
    }
}
