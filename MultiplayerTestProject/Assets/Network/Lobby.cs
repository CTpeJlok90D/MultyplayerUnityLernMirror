using UnityEngine;
using Mirror;

public class Lobby : NetworkManager
{
    [Header("Lobby")]
    [SerializeField] private GameObject playerSettingsUI;
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        playerSettingsUI.SetActive(true);
    }

    public void SpawnPlayer()
    {
        NetworkClient.AddPlayer();
    }
}
