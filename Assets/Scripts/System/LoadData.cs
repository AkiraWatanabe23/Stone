using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// csvデータのロード
/// </summary>
public class LoadData : MonoBehaviour
{
    [SerializeField] private GameObject _boardPrefab_one = default;
    [SerializeField] private GameObject _boardPrefab_two = default;

    private TextAsset _file = default;
    private readonly List<string[]> _datas = new();

    private void Awake()
    {
        LoadCsv();
    }

    private void Start()
    {
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
            _datas.Add(line);
        }
        Debug.Log("Finished.");
    }

    /// <summary>
    /// ボードの初期設定
    /// </summary>
    private void BoardSet()
    {
        for (int i = 0; i < _datas.Count; i++)
        {
            for (int j = 0; j < _datas[i].Length; j++)
            {
                if (_datas[i][j] == "0")
                {
                    if ((i + j) % 2 == 0)
                        Instantiate(
                            _boardPrefab_one, new Vector3(i, 0, j), Quaternion.identity);
                    else
                        Instantiate(
                            _boardPrefab_two, new Vector3(i, 0, j), Quaternion.identity);
                }
            }
        }
    }
}
