using Constants;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Turns _turn = default;
    [SerializeField] private Material[] _states = new Material[2];
    [SerializeField] private Material[] _selecting = new Material[2];
    [SerializeField] private GameObject[] _players = new GameObject[2];
    [SerializeField] private GameObject[] _boardStone = new GameObject[2];

    /// <summary> 盤面を数値で表現したもの </summary>
    private List<int[]> _board = new();
    /// <summary> 駒の配置、移動の際に使う判定用のList </summary>
    private List<int[]> _checkBoard = new();
    private readonly PlayableStones _stones = new();
    private readonly Judgment _judge = new();
    private StoneSelect _select = default;
    private UIManager _uiManager = default;

    public Turns Turn { get => _turn; protected set => _turn = value; }
    public List<int[]> Board { get => _board; set => _board = value; }
    public List<int[]> CheckBoard { get => _checkBoard; protected set => _checkBoard = value; }
    public MoveType Move { get; set; }

    private void Start()
    {
        _select = GetComponent<StoneSelect>();
        _uiManager = GetComponent<UIManager>();

        LoadData load = new();
        load.Stone[0] = _boardStone[0];
        load.Stone[1] = _boardStone[1];

        load.Init();
        _board = load.Board;
        _select.Selecting = _selecting[0];

        for (int i = 0; i < 5; i++)
        {
            _checkBoard.Add(new int[] { 0, 0, 0, 0, 0 });
        }

        _turn = Turns.WHITE;
        _select.Turn = _turn;
        _select.Player = _players[0];
    }

    private void Update()
    {
        //判定
        if (_select.IsSwitch)
        {
            _judge.Row(_board);
            _judge.Column(_board);
            _judge.Diagonal(_board);
            _select.IsSwitch = false;

            _select.BoardFresh();
            SwitchTurn();
        }
    }

    /// <summary>
    /// ターン切り替え時に呼び出す
    /// </summary>
    private void SwitchTurn()
    {
        //判定用のListをリセット
        for (int i = 0; i < 5; i++)
        {
            _checkBoard[i] = new int[] { 0, 0, 0, 0, 0 };
        }

        if (_turn == Turns.WHITE)
        {
            _select.Selecting = _selecting[1];
            _select.Player = _players[1];
            _turn = Turns.BLACK;
        }
        else
        {
            _select.Selecting = _selecting[0];
            _select.Player = _players[0];
            _turn = Turns.WHITE;
        }
        _select.Turn = _turn;
        _uiManager.MoveSelect.gameObject.SetActive(true);
        Debug.Log("ターンを切り替えます");
    }

    /// <summary>
    /// 行動選択(UIで呼び出す)
    /// </summary>
    public void PlayerMovement(int num)
    {
        switch (num)
        {
            case 1:
                Movement(0);
                break;
            case 2:
                Movement(1);
                break;
            case 3:
                //パス（何もせずにターンを切り替える）
                _select.IsSwitch = true;
                break;
        }
    }

    private void Movement(int num)
    {
        string tag = "";
        if (num == 0)
        {
            //配置可能なマスの判定
            _checkBoard = _stones.SettableStones(_board);
            tag = Consts.STONE_TAG;
        }
        else if (num == 1)
        {
            //移動可能な駒の探索
            //1,動かす駒を選ぶ
            _checkBoard = _stones.MovableStones(_board, _turn);
            tag = _turn == Turns.WHITE ?
                  Consts.PLAYER_ONE_TAG : Consts.PLAYER_TWO_TAG;
            //2,移動するマスを選ぶ
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (_checkBoard[i][j] == 1)
                {
                    Consts.FindWithVector(new Vector3(i, num, j), tag).
                        GetComponent<MeshRenderer>().material = _states[0];
                }
            }
        }
        Movable();
    }

    private bool Movable()
    {
        if (!_select.IsSelect)
        {
            _select.IsMovable = true;
        }
        else
        {
            return false;
        }
        return _select.IsMovable;
    }
}
