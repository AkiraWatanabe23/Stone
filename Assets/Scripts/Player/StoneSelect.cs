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

    }

    private void Select()
    {
        KeyCode input = default;

        switch (input)
        {
            case KeyCode.UpArrow:
            case KeyCode.W:
                break;
            case KeyCode.DownArrow:
            case KeyCode.D:
                break;
            case KeyCode.LeftArrow:
            case KeyCode.A:
                break;
            case KeyCode.RightArrow:
            case KeyCode.S:
                break;
        }
    }
}
