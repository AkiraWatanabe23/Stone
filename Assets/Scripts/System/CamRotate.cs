using System.Collections;
using UnityEngine;

/// <summary>
/// ゲーム中にカメラを回転させる
/// </summary>
public class CamRotate : MonoBehaviour
{
    [Tooltip("カメラの回転の基準となる座標")]
    [SerializeField] private Vector3 _pos = new(0, 0, 0);
    [Tooltip("回転速度")]
    [SerializeField] private Vector2 _rotateSpeed = Vector2.zero;

    private void Update()
    {
        //他のArrowキーを使う場合と区別するため、Shiftを押しながらにする
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            float angleX = Input.GetAxis("Horizontal") * _rotateSpeed.x * 0.1f;

            transform.RotateAround(_pos, Vector3.up, angleX);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(VerRotate(30f));
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(VerRotate(60f));
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(VerRotate(90f));
        }
    }

    private IEnumerator VerRotate(float value)
    {
        float x = Mathf.Round(transform.localEulerAngles.x);
        float upValue = 0f;
        float rotLimit = Mathf.Abs(x - value) > 30 ? 60f : 30f;

        if (x < value)
        {
            do
            {
                transform.RotateAround(_pos, Vector3.right, Time.deltaTime * _rotateSpeed.y);
                upValue += Time.deltaTime * _rotateSpeed.y;
                yield return null;
            }
            while (upValue < rotLimit);
        }
        else if (x > value)
        {
            do
            {
                transform.RotateAround(_pos, Vector3.right, Time.deltaTime * (-_rotateSpeed.y));
                upValue += Time.deltaTime * _rotateSpeed.y;
                yield return null;
            }
            while (upValue < rotLimit);
        }
    }
}
