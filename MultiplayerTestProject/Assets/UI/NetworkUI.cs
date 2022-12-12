using Mirror;
using UnityEngine;

public class NetworkUI : MonoBehaviour
{
    private NetworkManager networkManager => NetworkManager.singleton;

    public void OnConnectClick()
    {
        networkManager.StartClient();
        HideInteface();
    }

    public void OnHostClick()
    {
        networkManager.StartHost();
        HideInteface();
    }

    public void OnIpChanged(string newIP)
    {
        networkManager.networkAddress = newIP;
    }

    private void HideInteface()
    {
        gameObject.SetActive(false);
    }
}
