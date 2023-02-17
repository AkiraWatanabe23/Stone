using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadData : MonoBehaviour
{
    [SerializeField] private string _fileName = default;

    private Dictionary<int, int> _dic = new();

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        LoadCsv();
        Dump();
    }

    public void LoadCsv()
    {
        string filePath = Application.persistentDataPath + "/" + _fileName;

        using (StreamReader fs = new StreamReader(filePath))
        {
            fs.ReadLine();  // 一行飛ばす

            while (true)
            {
                // 一行ずつ読みこんで処理する
                string line = fs.ReadLine();

                // line に何も入っていなかったら終わったとみなして処理を終わる
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                var row = line.Split(',');
                _dic.Add(int.Parse(row[0]), int.Parse(row[1]));
            }
        }

        Debug.Log("Finished.");
    }

    public void Dump()
    {
        foreach (var row in _dic)
        {
            Debug.Log($"{row.Key} : {row.Value}");
        }
    }
}
