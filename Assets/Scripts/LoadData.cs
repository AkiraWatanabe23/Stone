using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadData : MonoBehaviour
{
    private TextAsset _file = default;
    private readonly List<string[]> _datas = new();

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        LoadCsv();
        Dump();
    }

    /// <summary>
    /// データロード
    /// </summary>
    public void LoadCsv()
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
    /// 確認用
    /// </summary>
    private void Dump()
    {
        for (int i = 0; i < _datas.Count; i++)
        {
            for (int j = 0; j < _datas[i].Length; j++)
            {
                Debug.Log(_datas[i][j]);
            }
        }
    }
}
