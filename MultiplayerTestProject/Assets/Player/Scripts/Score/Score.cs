using Mirror;
using UnityEngine.Events;
using UnityEngine;

public class Score : NetworkBehaviour
{
    [SyncVar]
    private int _current;
    [SerializeField] private UnityEvent<int> _currentChanged = new();

    public int Current => _current;
    public UnityEvent<int> CurrentChanged => _currentChanged;

    [Command]
    public void CommandSetCurrent(int value)
    {
        _current = value;
        ClientRPCSetCurrent(value);
    }

    [ClientRpc]
    private void ClientRPCSetCurrent(int value)
    {
        _current = value;
        _currentChanged.Invoke(value);
    }
}
