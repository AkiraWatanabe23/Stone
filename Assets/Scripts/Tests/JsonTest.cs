using UnityEngine;
using System.IO;

[System.Serializable]
public class JsonTest
{
    [SerializeField] private int _hp = 100;
    [SerializeField] private int _attack = 10;
    [SerializeField] private float _speed = 1f;

    public int HP { get => _hp; set => _hp = value; }
    public int Attack { get => _attack; set => _attack = value; }
    public float Speed { get => _speed; set => _speed = value; }

    public void SaveToJson()
    {
        JsonTest json = new()
        {
            HP = _hp,
            Attack = _attack,
            Speed = _speed
        };

        SaveDatas(json);
    }

    public void LoadFromJson()
    {
        JsonTest json = LoadDatas();

        Debug.Log(json.HP);
        Debug.Log(json.Attack);
        Debug.Log(json.Speed);
    }

    /// <summary>
    /// ファイルへの出力(データの保存)
    /// </summary>
    /// <param name="test"> 保存するオブジェクト </param>
    private void SaveDatas(JsonTest test)
    {
        string jsonStr = JsonUtility.ToJson(test);
        StreamWriter writer =
            new(Application.dataPath + "/savedata.json", false);

        writer.Write(jsonStr);
        writer.Flush();
        writer.Close();
        Debug.Log(jsonStr);
    }

    /// <summary>
    /// ファイルからデータを読み込む
    /// </summary>
    /// <returns> 読み込んだデータ </returns>
    private JsonTest LoadDatas()
    {
        StreamReader reader =
            new(Application.dataPath + "/savedata.json");
        string dataStr = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<JsonTest>(dataStr);
    }
}
