﻿using Constants;
using UnityEngine;

[System.Serializable]
public class PieceSelect
{
    [SerializeField] private Material[] _default = new Material[3];

    private GameManager _manager = default;

    public void Start(GameManager manager, Material[] def)
    {
        _manager = manager;
        _default = def;
    }

    /// <summary> Playerの入力 </summary>
    public int Select(string input, int index)
    {
        int num = 0;
        switch (input)
        {
            case "w":
            case "s":
            case "a":
            case "d":
                num = SelectPiece(index);
                break;
        }
        return num;
    }

    private int SelectPiece(int index)
    {
        GameObject piece = null;
        if (_manager.Turn == Turns.WHITE)
            piece =
                Consts.FindWithVector(_manager.White[index].transform.position);
        else if (_manager.Turn == Turns.BLACK)
            piece =
                Consts.FindWithVector(_manager.Black[index].transform.position);
        var mat = piece.GetComponent<MeshRenderer>().material;

        if (mat.name.Contains("Orange") || mat.name.Contains("Blue"))
        {
            mat = _default[2];
        }
        else
        {
            Debug.Log(mat.name);
        }
        piece.GetComponent<MeshRenderer>().material = mat;

        if (_manager.Turn == Turns.WHITE)
        {
            if (index + 1 <= _manager.White.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }
        else if (_manager.Turn == Turns.BLACK)
        {
            if (index + 1 <= _manager.Black.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }

        if (_manager.Turn == Turns.WHITE)
            Consts.FindWithVector(_manager.White[index].transform.position).
                GetComponent<MeshRenderer>().material = _manager.Selecting;
        else if (_manager.Turn == Turns.BLACK)
            Consts.FindWithVector(_manager.Black[index].transform.position).
                GetComponent<MeshRenderer>().material = _manager.Selecting;

        return index;
    }
}