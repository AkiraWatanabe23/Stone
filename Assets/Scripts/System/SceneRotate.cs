using UnityEngine;

public class SceneRotate : MonoBehaviour
{
    [SerializeField] private float _rotateX = 1f;
    [SerializeField] private float _rotateY = 1f;
    [SerializeField] private float _rotateZ = 1f;

    private void Update()
    {
        gameObject.transform.Rotate(
            new Vector3(_rotateX, _rotateY, _rotateZ) * Time.deltaTime);
    }
}
