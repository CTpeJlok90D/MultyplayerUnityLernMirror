using UnityEngine;
using Mirror;

public class Lobby : NetworkManager
{
    [Header("Lobby")]
    [SerializeField] private GameObject playerSettingsUI;

    public void SpawnPlayer()
    {
        NetworkClient.AddPlayer();
    }
}
