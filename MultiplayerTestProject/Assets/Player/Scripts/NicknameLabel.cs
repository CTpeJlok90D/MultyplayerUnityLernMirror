using TMPro;
using UnityEngine;

public class NicknameLabel : MonoBehaviour
{
    public Transform _camera;

    [SerializeField] private Player _owner;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Vector3 _offcet;

    private void Update()
    {
        transform.position = _owner.transform.position + _offcet;
        transform.forward = Camera.main.transform.position;
        _label.text = _owner.Nickname;
    }

    private void OnValidate()
    {
        transform.position = _owner.transform.position + _offcet;
    }
}
