using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _turn = default;
    [SerializeField] private Image _moveSelect = default;

    private GameManager _manager = default;

    public Image MoveSelect { get => _moveSelect; protected set => _moveSelect = value; }

    void Start()
    {
        _manager = GetComponent<GameManager>();
        _moveSelect.gameObject.SetActive(true);
    }

    private void Update()
    {
        _turn.text = _manager.Turn.ToString();
    }
}
