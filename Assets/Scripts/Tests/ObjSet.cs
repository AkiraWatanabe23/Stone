using Constants;
using UnityEngine;

public class ObjSet : MonoBehaviour
{
    [SerializeField] private Turns _turn = default;
    [SerializeField] private GameObject[] _players = new GameObject[2];

    private void Start()
    {
        _turn = Turns.WHITE;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_turn == Turns.WHITE)
            {
                Instantiate(_players[0]);
                _turn = Turns.BLACK;
                Debug.Log("Set White");
            }
            else
            {
                Instantiate(_players[1]);
                _turn = Turns.WHITE;
                Debug.Log("Set Black");
            }
        }
    }
}
