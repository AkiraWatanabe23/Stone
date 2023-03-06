using Constants;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Material _state = default;
    [SerializeField] private Material[] _pieceCol = new Material[2];
    [SerializeField] private GameObject[] _players = new GameObject[2];
    [SerializeField] private GameObject[] _enhancementPlayers = new GameObject[2];
    [SerializeField] private GameObject[] _boardStone = new GameObject[2];
    [SerializeField] private WinningStaging _staging = new();

    private Turns _turn = Turns.RED;
    /// <summary> 盤面を数値で表現したもの </summary>
    private List<int[]> _board = new();
    /// <summary> 駒の配置、移動の際に使う判定用のList </summary>
    private List<int[]> _checkBoard = new();
    private readonly PlayableStones _stones = new();
    private readonly Judgment _judge = new();
    private Selecting _select = default;
    private UIManager _uiManager = default;
    private int _whiteCount = 0;
    private int _blackCount = 0;

    public Turns Turn { get => _turn; protected set => _turn = value; }
    public List<int[]> Board { get => _board; set => _board = value; }
    public List<int[]> CheckBoard { get => _checkBoard; protected set => _checkBoard = value; }
    public GameObject Player { get; protected set; }
    public GameObject EnhancementPlayer { get; protected set; }
    public Material Selecting { get; protected set; }
    public MoveType Move { get; protected set; }
    public List<GameObject> White { get; set; }
    public List<GameObject> Black { get; set; }

    private void Start()
    {
        _select = GetComponent<Selecting>();
        _uiManager = GetComponent<UIManager>();

        LoadData load = new();
        load.Stone[0] = _boardStone[0];
        load.Stone[1] = _boardStone[1];

        _staging.Start();
        load.Start();
        _board = load.Board;
        Selecting = _pieceCol[0];
        White = new();
        Black = new();

        for (int i = 0; i < 5; i++)
        {
            _checkBoard.Add(new int[] { 0, 0, 0, 0, 0 });
        }
        Player = _players[0];
        EnhancementPlayer = _enhancementPlayers[0];
    }

    private void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(_staging.Winning());
        }

        //判定
        if (_select.IsSwitch)
        {
            var resultRow = _judge.Row(_board);
            var resultColumn = _judge.Column(_board);
            var resultDiagonal = _judge.Diagonal(_board);

            //勝利判定を行い、どちらかが勝ったら勝利演出
            if (resultRow != JudgeResult.DRAW ||
                resultColumn != JudgeResult.DRAW ||
                resultDiagonal != JudgeResult.DRAW)
            {
                if (resultRow == JudgeResult.RED_WIN ||
                    resultColumn == JudgeResult.RED_WIN ||
                    resultDiagonal == JudgeResult.RED_WIN)
                {
                    //赤が勝った場合
                    _uiManager.Winning.text = "Player Red Win!!";
                }
                else
                {
                    //黒が勝った場合
                    _uiManager.Winning.text = "Player Black Win!!";
                }
                StartCoroutine(_staging.Winning());
            }
            _select.IsSwitch = false;

            _select.BoardFresh();

            //↓ターン切り替え時の処理
            //判定用のListをリセット
            for (int i = 0; i < 5; i++)
            {
                _checkBoard[i] = new int[] { 0, 0, 0, 0, 0 };
            }

            if (_turn == Turns.RED)
            {
                _uiManager.Anim[0].enabled = false;
                _uiManager.Anim[1].enabled = true;

                Selecting = _pieceCol[1];
                Player = _players[1];
                EnhancementPlayer = _enhancementPlayers[1];
                _turn = Turns.BLACK;
            }
            else
            {
                _uiManager.Anim[0].enabled = true;
                _uiManager.Anim[1].enabled = false;

                Selecting = _pieceCol[0];
                Player = _players[0];
                EnhancementPlayer = _enhancementPlayers[0];
                _turn = Turns.RED;
            }
            Move = MoveType.DEFAULT;
            _uiManager.MoveSelect.gameObject.SetActive(true);
        }
    }

    /// <summary> 行動選択(UIで呼び出す) </summary>
    public void PlayerMovement(int num)
    {
        switch (num)
        {
            case 1:
                if (_turn == Turns.RED)
                {
                    if (_whiteCount == Consts.PIECE_LIMIT)
                    {
                        Debug.Log("これ以上駒を配置できません");
                        _uiManager.MoveSelect.gameObject.SetActive(true);
                        break;
                    }
                }
                else
                {
                    if (_blackCount == Consts.PIECE_LIMIT)
                    {
                        Debug.Log("これ以上駒を配置できません");
                        _uiManager.MoveSelect.gameObject.SetActive(true);
                        break;
                    }
                }

                Move = MoveType.SET;
                Movement(0);
                break;
            case 2:
                Move = MoveType.MOVE;
                Movement(1);
                break;
            case 3:
                //パス（何もせずにターンを切り替える）
                _select.IsSwitch = true;
                break;
        }
    }

    public void Movement(int num)
    {
        if (num == 0)
        {
            //配置可能なマスの判定
            _checkBoard = _stones.SettableStones(_board);
        }
        else if (num == 1)
        {
            //移動可能な駒の探索
            if (_select.SelectedPiece == null)
            {
                //1,動かす駒を選ぶ
                Debug.Log("search stone...");
                _checkBoard = _stones.MovableStones(_board, _turn);
            }
            else
            {
                //2,移動するマスを選ぶ
                _checkBoard = _stones.MovablePositions(_board, _select.SelectedPiece, _turn);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (_checkBoard[i][j] == 1)
                {
                    Consts.FindWithVector(new Vector3(i, 0f, j)).
                        GetComponent<MeshRenderer>().material = _state;
                }
            }
        }
        Movable();
    }

    private bool Movable()
    {
        if (!_select.IsSelect)
            _select.IsMovable = true;
        else
            return false;

        return _select.IsMovable;
    }

    /// <summary> 駒を配置して、盤面情報を更新する </summary>
    public void PieceSetting(GameObject piece, Vector3 pos)
    {
        if (_turn == Turns.RED)
        {
            _board[(int)pos.x][(int)pos.z] = 1;
            _whiteCount++;
            White.Add(piece);
        }
        else if (_turn == Turns.BLACK)
        {
            _board[(int)pos.x][(int)pos.z] = -1;
            _blackCount++;
            Black.Add(piece);
        }
    }

    public void CountDown(Vector3 pos)
    {
        if (_turn == Turns.RED)
            _whiteCount -=
                _board[(int)pos.x][(int)pos.z];
        else if (_turn == Turns.BLACK)
            _blackCount -=
                _board[(int)pos.x][(int)pos.z];
    }
}
