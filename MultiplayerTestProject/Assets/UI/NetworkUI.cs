using Mirror;
using UnityEngine;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private NetworkManager _manager;

    public void OnConnectClick()
    {
        _manager.StartClient();
        HideInteface();
    }

    public void OnHostClick()
    {
        _manager.StartHost();
        HideInteface();
    }

    public void OnIpChanged(string newIP)
    {
        _manager.networkAddress = newIP;
    }

    private void HideInteface()
    {
        gameObject.SetActive(false);
    }
}
