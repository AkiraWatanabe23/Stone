using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
/// <summary>
/// csvデータのロード
/// </summary>
public class LoadData
{
    [SerializeField] private GameObject _prefabOne = default;
    [SerializeField] private GameObject _prefabTwo = default;

    private TextAsset _file = default;
    private List<string[]> _board = new();

    public List<string[]> Board { get => _board; set => _board = value; }

    public void Awake()
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
            var line = reader.ReadLine().Split(',');
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
                if (_board[i][j] == "0")
                {
                    if ((i + j) % 2 == 0)
                        Object.Instantiate(
                            _prefabOne, new Vector3(i, 0, j), Quaternion.identity);
                    else
                        Object.Instantiate(
                            _prefabTwo, new Vector3(i, 0, j), Quaternion.identity);
                }
            }
        }
    }
}
