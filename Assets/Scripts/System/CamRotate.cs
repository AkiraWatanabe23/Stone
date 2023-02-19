using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    [SerializeField] Vector2 _rotateSpeed = new(0, 0);

    // マウス座標を格納する変数
    private Vector2 _lastMousePos = default;
    // カメラの角度を格納する変数（初期値に0,0を代入）
    private Vector2 _currentAngle = new(0, 0);
    private Camera _cam = default;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentAngle = _cam.transform.localEulerAngles;
            _lastMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            _currentAngle.y -=
                (Input.mousePosition.x - _lastMousePos.x) * _rotateSpeed.y;

            _currentAngle.x -=
                (Input.mousePosition.y - _lastMousePos.y) * _rotateSpeed.x;

            _cam.transform.localEulerAngles = _currentAngle;
            _lastMousePos = Input.mousePosition;
        }
    }
}
