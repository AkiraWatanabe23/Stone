using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
/// <summary>
/// csvデータのロード
/// </summary>
public class LoadData
{
    private TextAsset _file = default;
    private List<int[]> _board = new();
    private readonly GameObject[] _stone = new GameObject[2];

    public List<int[]> Board { get => _board; protected set => _board = value; }
    public GameObject[] Stone => _stone;

    public void Start()
    {
        LoadCsv();
        BoardSet();
    }

    /// <summary>
    /// データロード
    /// </summary>
    private void LoadCsv()
    {
        _file = Resources.Load("StartState") as TextAsset;
        StringReader reader = new(_file.text);
        //1行捨てる
        _ = reader.ReadLine();

        while (reader.Peek() != -1)
        {
            var line = Array.ConvertAll(reader.ReadLine().Split(','), int.Parse);
            _board.Add(line);
        }
        Debug.Log("Load finished.");
    }

    /// <summary>
    /// ボードの初期設定
    /// </summary>
    private void BoardSet()
    {
        for (int i = 0; i < _board.Count; i++)
        {
            for (int j = 0; j < _board[i].Length; j++)
            {
                if (_board[i][j] == 0)
                {
                    if ((i + j) % 2 == 0)
                    {
                        UnityEngine.Object.Instantiate(
                            _stone[0], new Vector3(i, 0, j), Quaternion.identity);
                    }
                    else
                    {
                        UnityEngine.Object.Instantiate(
                            _stone[1], new Vector3(i, 0, j), Quaternion.identity);
                    }
                }
            }
        }
    }
}
