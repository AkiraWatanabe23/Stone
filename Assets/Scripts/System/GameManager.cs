using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Turns _turn = default;
    [SerializeField] private LoadData _load = default;

    private readonly PlayableStones _stones = default;
    private readonly Judgment _judge = default;
    private List<string[]> _board = new();
    /// <summary> ターンの切り替え </summary>
    private bool _isSwitch = false;

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
        _load.Update();
        //盤面が更新されたら、それを反映する
        if (_board.All(n => _load.Board.Any(i => i == n)) &&
            _load.Board.All(n => _board.Any(i => i == n)))
        {
            _board = _load.Board;
        }

        //判定
        if (_isSwitch)
        {
            _judge.Row(_board);
            _judge.Column(_board);
            _judge.Diagonal(_board);
            _isSwitch = false;
        }
    }
}
