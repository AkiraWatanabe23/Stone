using Constants;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Turns _turn = default;
    [SerializeField] private LoadData _load = default;

    private Judgment _judge = default;
    private List<string[]> _board = new();
    /// <summary> ターンの切り替え </summary>
    private bool _isSwitch = false;
    private bool _isWinning = false;

    private void Awake()
    {
        _load.Awake();
    }

    private void Start()
    {
        //スタートをランダムにする
        _turn = (Turns)Enum.ToObject(typeof(Turns), UnityEngine.Random.Range(0, (int)Turns.COUNT));
        Debug.Log(_turn);
    }

    private void Update()
    {
        _load.Update();

        //判定
        if (_isSwitch)
        {
            _judge.Row();
            _judge.Column();
            _judge.Diagonal();
            _isSwitch = false;
        }
    }
}
