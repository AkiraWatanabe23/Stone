using UnityEngine;

/// <summary>
/// ゲーム中にカメラを回転させる
/// </summary>
public class CamRotate : MonoBehaviour
{
    [Tooltip("カメラの回転の基準となる座標")]
    [SerializeField] private Vector3 _pos = new(0, 0, 0);
    [Tooltip("回転速度(各回転方向ごと)")]
    [SerializeField] private Vector2 _rotateSpeed = new(0, 0);

    private void Update()
    {
        //他のArrowキーを使う場合と区別するため、LeftShiftを押しながらにする
        //（回転に制限をつけた方が良さそう）
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float angleX = Input.GetAxis("Horizontal") * _rotateSpeed.x;
            float angleY = Input.GetAxis("Vertical") * _rotateSpeed.y;

            transform.RotateAround(_pos, Vector3.up, -angleX);
            transform.RotateAround(_pos, Vector3.right, angleY);
        }
    }
}
