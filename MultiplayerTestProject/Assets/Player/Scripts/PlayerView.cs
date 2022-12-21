using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _playerView;
    [SerializeField] private Transform _shoulders;
    [SerializeField] private Player _owner;

    private void LateUpdate()
    {
        _animator.SetBool("Running", _owner.IsMoving);
        if (_owner.IsMoving)
        {
            _playerView.forward = _owner.CurrentMoveDirection;
        }
    }
}
