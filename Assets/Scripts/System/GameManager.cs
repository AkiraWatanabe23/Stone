using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Turns _turn = default;
    [SerializeField] private LoadData _load = default;
    [SerializeField] private PlayableStones _stones = default;
    [SerializeField] private Judgment _judge = default;

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
                PassTurn();
                break;
        }
    }

    private void PassTurn()
    {
        _isSwitch = true;
    }
}
