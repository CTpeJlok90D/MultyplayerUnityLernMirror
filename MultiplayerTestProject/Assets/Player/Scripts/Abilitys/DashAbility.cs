using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using System.Collections;

public class DashAbility : NetworkBehaviour, IAbility
{
    [SerializeField] private float _dituration = 0.5f;
    [SerializeField] private float _dashSpeed = 0.4f;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Player _player;
    [SerializeField] private float _cooldown = 2f;
    [SerializeField] private TriggerEvent _dashTrigger;
    [SerializeField] private Score _score;

    private float _currentCooldown = 0;

    #region Commands
    [Command]
    private void CommandExecute()
    {
        Execute();
        ClientRPCExecute();
    }

    [ClientRpc]
    private void ClientRPCExecute()
    {
        Execute();
    }
    #endregion

    private void OnEnable()
    {
        _dashTrigger.Enter.AddListener(Hit);
    }
    private void OnDisable()
    {
        _dashTrigger.Enter.RemoveListener(Hit);
    }

    public void Hit(Collider other)
    {
        if (!isClient || !isLocalPlayer)
        {
            return;
        }
        if (other.TryGetComponent(out Score score) && score != _score)
        {
            _score.CommandSetCurrent(_score.Current + 1);
        }
    }

    public void Use(InputAction.CallbackContext context)
    {
        if (!context.started || !isLocalPlayer || !isClient)
        {
            return;
        }
        CommandExecute();
        Execute();
    }

    public void Execute()
    {
        if (_currentCooldown <= 0 && _player.IsMoving)
        {
            StartCoroutine(ExecuteCorutine(Time.deltaTime));
            StartCoroutine(CooldownCorutine(Time.deltaTime));
        }
    }

    private IEnumerator ExecuteCorutine(float delta)
    {
        _dashTrigger.Collider.enabled = true;
        if (isLocalPlayer && isClient)
        {
            _player.CommandSetCanMove(false);
        }
        for (float _currentDituration = _dituration; _currentDituration > 0; _currentDituration -= delta)
        {
            _characterController.Move(_player.CurrentMoveDirection * _dashSpeed * delta);
            yield return null;
        }
        if (isLocalPlayer && isClient)
        {
            _player.CommandSetCanMove(true);
        }
        _dashTrigger.Collider.enabled = false;
    }

    private IEnumerator CooldownCorutine(float delta)
    {
        for (_currentCooldown = _cooldown; _currentCooldown > 0; _currentCooldown -= delta)
        {
            yield return null;
        }
    }
}