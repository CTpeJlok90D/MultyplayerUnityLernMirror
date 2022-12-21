using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class PlayerReady : NetworkBehaviour
{
    [SyncVar][SerializeField] private bool _state;
    [SerializeField] private UnityEvent _valueChanged;

    public bool State 
    { 
        get
        {
            return _state;
        }
        set 
        {
            _state = value;
            _valueChanged.Invoke();
        }
    }
    public UnityEvent ValueChanged => _valueChanged;

    [ClientRpc]
    private void ClientRPCSetReady(bool value)
    {
        State = value; 
    }

    [Command]
    public void CommandSetReady(bool value)
    {
        State = value;
        ClientRPCSetReady(value);
    }

    public void ToggleReady()
    {
        CommandSetReady(!State);
    }
}
