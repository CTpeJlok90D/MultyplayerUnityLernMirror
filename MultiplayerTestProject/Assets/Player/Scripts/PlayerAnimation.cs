using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _playerView;
    [SerializeField] private Transform _shoulders;
    [SerializeField] private Player _owner;

    private InputAction.CallbackContext _currentContext;

    private bool _isMoving => _currentContext.started || _currentContext.performed;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (_owner.IsLocalPlayer == false)
        {
            return;
        }
        _currentContext = context;
        _animator.SetBool("Running", _isMoving);
    }

    public void LateUpdate()
    {
        if ((_isMoving && _owner.IsLocalPlayer) == false)
        {
            return;
        }
        _playerView.rotation = Quaternion.Euler(new Vector3(_playerView.eulerAngles.x, _shoulders.eulerAngles.y, _playerView.eulerAngles.z));
    }
}
