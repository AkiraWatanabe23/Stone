using Constants;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Turns _turn = default;
    [SerializeField] private LoadData _load = default;
    [SerializeField] private StoneSelect _select = default;
    [SerializeField] private Material[] _states = new Material[2];

    private List<int[]> _board = new();
    private List<GameObject[]> _objs = new();
    private readonly PlayableStones _stones = new();
    private readonly Judgment _judge = new();

    public MoveType Move { get; set; }

    private void Awake()
    {
        _load.Awake();
        _stones.Awake();
    }

    private void Start()
    {
        _board = _load.Board;
        _select.Board = _load.Board;
        _objs = _load.BoardState;
        _select.BoardState = _load.BoardState;

        _turn = Turns.WHITE;
    }

    private void Update()
    {
        _select.Update();

        //判定
        if (_select.IsSwitch)
        {
            //盤面が更新されたら、それを反映する
            if (!_board.All(n => _load.Board.Any(i => i == n)) &&
                !_load.Board.All(n => _board.Any(i => i == n)))
            {
                _board = _load.Board;
                Debug.Log("盤面に変更がありました。");
            }

            _judge.Row(_board);
            _judge.Column(_board);
            _judge.Diagonal(_board);
            _select.IsSwitch = false;

            _select.BoardFresh();
            _turn =
                _turn == Turns.WHITE ?
                Turns.BLACK : Turns.WHITE;
            Debug.Log("ターンを切り替えます");
        }
    }

    public void PlayerMovement(int num)
    {
        List<int[]> checking = new();

        switch (num)
        {
            case 1:
                //配置可能なマスの判定
                checking = _stones.SettableStones(_board);
                _select.Board = checking;
                for (int i = 0; i < _objs.Count; i++)
                {
                    for (int j = 0; j < _objs[i].Length; j++)
                    {
                        if (checking[i][j] == 1)
                            _objs[i][j].GetComponent<MeshRenderer>().material = _states[0];
                    }
                }
                break;
            case 2:
                //移動可能なマスの判定
                _stones.MovableStones(gameObject, _board);
                break;
            case 3:
                _select.IsSwitch = true;
                break;
        }
    }
}
