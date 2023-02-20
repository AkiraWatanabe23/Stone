using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    [SerializeField] private Vector3 _pos = new(0, 0, 0);
    [SerializeField] private Vector2 _rotateSpeed = new(0, 0);

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float angleX = Input.GetAxis("Horizontal") * _rotateSpeed.x;
            float angleY = Input.GetAxis("Vertical") * _rotateSpeed.y;

            transform.RotateAround(_pos, Vector3.up, -angleX);
            transform.RotateAround(_pos, Vector3.right, angleY);
        }
    }
}
