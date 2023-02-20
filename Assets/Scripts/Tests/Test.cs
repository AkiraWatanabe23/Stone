using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private JsonTest _json = default;

    public void SaveToJson()
    {
        _json.SaveToJson();
    }

    public void LoadFromJson()
    {
        _json.LoadFromJson();
    }
}
