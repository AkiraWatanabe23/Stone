using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSelect : MonoBehaviour
{
    private void Start()
    {

    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Select(Input.inputString);
        }
    }

    private void Select(string input)
    {
        switch (input)
        {
            case "w":
                Debug.Log("input up");
                MoveStone(0);
                break;
            case "d":
                Debug.Log("input down");
                MoveStone(1);
                break;
            case "a":
                Debug.Log("input left");
                MoveStone(2);
                break;
            case "s":
                Debug.Log("input right");
                MoveStone(3);
                break;
        }
    }

    private void MoveStone(int dir)
    {
        switch (dir)
        {
            //上方向
            case 0:
                break;
            //下方向
            case 1:
                break;
            //左方向
            case 2:
                break;
            //右方向
            case 3:
                break;
        }
    }
}
