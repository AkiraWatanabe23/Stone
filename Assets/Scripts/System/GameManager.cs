using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _players = new GameObject[2];
    [SerializeField] private Turns _turn = default;
    [SerializeField] private LoadData _load = default;

    private List<int[]> _board = new();
    /// <summary> ターンの切り替え </summary>
    private bool _isSwitch = false;
    private readonly PlayableStones _stones = new();
    private readonly Judgment _judge = new();

    private void Awake()
    {
        _load.Awake();
        _stones.Awake();
    }

    private void Start()
    {
        _board = _load.Board;
        //スタートをランダムにする
        _turn = (Turns)Enum.ToObject(typeof(Turns), UnityEngine.Random.Range(0, (int)Turns.COUNT));
        Debug.Log(_turn);
    }

    private void Update()
    {
        //判定
        if (_isSwitch)
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
            _isSwitch = false;
        }
    }

    public void PlayerMovement(int num)
    {
        switch (num)
        {
            case 1:
                _stones.SettableStones(_board);
                break;
            case 2:
                _stones.MovableStones(gameObject, _board);
                break;
            case 3:
                _isSwitch = true;
                break;
        }
    }
}
