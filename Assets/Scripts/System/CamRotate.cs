using UnityEngine;

/// <summary>
/// ゲーム中にカメラを回転させる
/// </summary>
public class CamRotate : MonoBehaviour
{
    [Tooltip("カメラの回転の基準となる座標")]
    [SerializeField] private Vector3 _pos = new(0, 0, 0);
    [Tooltip("回転速度")]
    [SerializeField] private float _rotateSpeed = 1f;

    private void Update()
    {
        //他のArrowキーを使う場合と区別するため、LeftShiftを押しながらにする
        //（回転に制限をつけた方が良さそう）
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float angleX = Input.GetAxis("Horizontal") * _rotateSpeed;

            transform.RotateAround(_pos, Vector3.up, angleX);
        }
    }

    public void RotX(float value)
    {
        //TODO：この部分に制限をつける必要有
        transform.RotateAround(_pos, Vector3.right, value);
    }
}
