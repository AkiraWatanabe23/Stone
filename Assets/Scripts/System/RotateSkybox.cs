using UnityEngine;

/// <summary>
/// Skyboxを回転させる
/// </summary>
public class RotateSkybox : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 0.1f;

    private float _angle = 0f;

    private void Update()
    {
        _angle += _rotateSpeed;
        RenderSettings.skybox.SetFloat("_Rotation", _angle);
    }
}
