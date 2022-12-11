using UnityEngine;

public class RandomColor : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    private Color _color;

    private void Awake()
    {
        _color = new Color(Random.value, Random.value, Random.value);
        _meshRenderer.material.color = _color;
    }
}
