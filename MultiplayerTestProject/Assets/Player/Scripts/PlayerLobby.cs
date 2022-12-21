using Mirror;
using UnityEngine;

public class PlayerLobby : NetworkBehaviour
{
    private static PlayerLobby _instance;

    public static PlayerLobby Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }
}
