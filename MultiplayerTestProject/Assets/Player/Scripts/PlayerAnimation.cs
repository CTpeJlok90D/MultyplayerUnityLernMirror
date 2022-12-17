using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _playerView;
    [SerializeField] private Transform _shoulders;
    [SerializeField] private Player _owner;

    private bool _isMoving => _owner.CurrentMoveDirection != Vector3.zero;

    public void LateUpdate()
    {
        _animator.SetBool("Running", _isMoving);
        if (_isMoving)
        {
            _playerView.rotation = Quaternion.Euler(new Vector3(_playerView.eulerAngles.x, _shoulders.eulerAngles.y, _playerView.eulerAngles.z));
        }
    }
}
